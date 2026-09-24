using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Practo.HospitalFinder.SeleniumBDD.Models;
using System.Globalization;

namespace Practo.HospitalFinder.SeleniumBDD.Pages;

public class HospitalSearchPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _hospitalCards =
        By.CssSelector(".c-estb-card");

    public HospitalSearchPage(IWebDriver driver)
    {
        _driver = driver;

        _wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(10));
    }

    public void NavigateToBangaloreHospitals()
    {
        _driver.Navigate().GoToUrl(
            "https://www.practo.com/bangalore/hospitals");

        _wait.Until(driver =>
            driver.FindElements(_hospitalCards).Count > 0);
    }

    public IReadOnlyCollection<IWebElement> GetHospitalCards()
    {
        return _driver.FindElements(_hospitalCards);
    }

    public List<HospitalResult> GetHospitalResults()
    {
        var results = new List<HospitalResult>();

        var cards =
            _driver.FindElements(_hospitalCards);

        foreach (var card in cards)
        {
            var nameElement =
                card.FindElement(
                    By.CssSelector("h2.line-1"));

            var name =
                nameElement.Text.Trim();

            var location =
                card.FindElement(
                        By.CssSelector(".c-locality-info"))
                    .Text
                    .Trim();

            var rating =
                GetRating(card);

            var isOpen24x7 =
                card.FindElements(
                        By.XPath(
                            ".//*[normalize-space(text())='Open 24x7']"))
                    .Count > 0;

            var detailsLink =
                nameElement.FindElement(
                    By.XPath("./parent::a"));

            var detailsUrl =
                detailsLink.GetAttribute("href") ?? string.Empty;

            results.Add(
                new HospitalResult
                {
                    Name = name,
                    Location = location,
                    Rating = rating,
                    IsOpen24x7 = isOpen24x7,
                    DetailsUrl = detailsUrl
                });
        }

        return results;
    }

    public List<HospitalResult> GetCandidateHospitals(
        List<HospitalResult> hospitals)
    {
        return hospitals
            .Where(hospital =>
                hospital.IsOpen24x7 &&
                hospital.Rating.HasValue &&
                hospital.Rating.Value > 3.5)
            .ToList();
    }

    public string OpenHospitalDetails(
        HospitalResult hospital)
    {
        var originalWindow =
            _driver.CurrentWindowHandle;

        var existingWindows =
            _driver.WindowHandles.ToHashSet();

        var hospitalLink =
            _driver.FindElements(
                    By.CssSelector(".c-estb-card h2.line-1"))
                .Select(nameElement =>
                    nameElement.FindElement(
                        By.XPath("./parent::a")))
                .First(link =>
                    string.Equals(
                        link.GetAttribute("href"),
                        hospital.DetailsUrl,
                        StringComparison.OrdinalIgnoreCase));

        hospitalLink.Click();

        _wait.Until(driver =>
            driver.WindowHandles.Count >
            existingWindows.Count);

        var newWindow =
            _driver.WindowHandles
                .First(handle =>
                    !existingWindows.Contains(handle));

        _driver.SwitchTo()
            .Window(newWindow);

        _wait.Until(driver =>
            ((IJavaScriptExecutor)driver)
                .ExecuteScript(
                    "return document.readyState")
                ?.ToString() == "complete");

        return originalWindow;
    }

    public void CloseHospitalDetails(
        string originalWindow)
    {
        _driver.Close();

        _driver.SwitchTo()
            .Window(originalWindow);

        _wait.Until(driver =>
            driver.FindElements(_hospitalCards).Count > 0);
    }

    private static double? GetRating(
        IWebElement card)
    {
        var ratingElements =
            card.FindElements(
                By.CssSelector(
                    ".c-feedback .u-bold"));

        if (ratingElements.Count == 0)
        {
            return null;
        }

        var ratingText =
            ratingElements[0].Text.Trim();

        if (double.TryParse(
                ratingText,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out var rating))
        {
            return rating;
        }

        return null;
    }
}