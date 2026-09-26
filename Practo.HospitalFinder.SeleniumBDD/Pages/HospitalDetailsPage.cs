using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Practo.HospitalFinder.SeleniumBDD.Pages;

public class HospitalDetailsPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _doNotConsentButton =
        By.CssSelector(
            "button[aria-label='Do not consent']");

    private readonly By _readMoreInfo =
        By.CssSelector(
            "[data-qa-id='read_more_info']");

    private readonly By _amenityItems =
        By.CssSelector(
            "[data-qa-id='amenity_item']");

    public HospitalDetailsPage(IWebDriver driver)
    {
        _driver = driver;

        _wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(10));
    }

    public void DismissConsentPopupIfPresent()
    {
        try
        {
            var shortWait =
                new WebDriverWait(
                    _driver,
                    TimeSpan.FromSeconds(3));

            var button =
                shortWait.Until(driver =>
                {
                    var elements =
                        driver.FindElements(
                            _doNotConsentButton);

                    return elements.Count > 0 &&
                           elements[0].Displayed &&
                           elements[0].Enabled
                        ? elements[0]
                        : null;
                });

            button.Click();

            Console.WriteLine(
                "Consent popup dismissed.");
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine(
                "Consent popup not displayed.");
        }
    }

    public void ExpandHospitalInformation()
    {
        DismissConsentPopupIfPresent();

        var readMore =
            _wait.Until(driver =>
            {
                var elements =
                    driver.FindElements(
                        _readMoreInfo);

                return elements.Count > 0 &&
                       elements[0].Displayed &&
                       elements[0].Enabled
                    ? elements[0]
                    : null;
            });

        ScrollIntoView(readMore);

        readMore.Click();

        _wait.Until(driver =>
            driver.FindElements(
                _amenityItems).Count > 0);
    }

    public bool HasParking()
    {
        var amenities =
            _driver.FindElements(
                _amenityItems);

        var parking =
            amenities.FirstOrDefault(
                amenity =>
                    amenity.Text.Trim().Equals(
                        "Parking",
                        StringComparison.OrdinalIgnoreCase));

        if (parking is null)
        {
            return false;
        }

        ScrollIntoView(parking);

        return true;
    }

    private void ScrollIntoView(
        IWebElement element)
    {
        if (_driver is IJavaScriptExecutor js)
        {
            js.ExecuteScript(
                "arguments[0].scrollIntoView(" +
                "{behavior: 'auto', block: 'center'});",
                element);
        }
    }
}