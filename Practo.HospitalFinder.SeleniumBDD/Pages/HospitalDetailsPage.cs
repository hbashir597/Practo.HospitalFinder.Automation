using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Practo.HospitalFinder.SeleniumBDD.Pages;

public class HospitalDetailsPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By _readMoreInfo =
        By.CssSelector("[data-qa-id='read_more_info']");

    private readonly By _amenityItems =
        By.CssSelector("[data-qa-id='amenity_item']");

    public HospitalDetailsPage(IWebDriver driver)
    {
        _driver = driver;

        _wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(10));
    }

    public void ExpandHospitalInformation()
    {
        var readMore =
            _wait.Until(driver =>
            {
                var elements =
                    driver.FindElements(_readMoreInfo);

                return elements.Count > 0 &&
                       elements[0].Displayed &&
                       elements[0].Enabled
                    ? elements[0]
                    : null;
            });

        readMore.Click();

        _wait.Until(driver =>
            driver.FindElements(_amenityItems).Count > 0);
    }

    public bool HasParking()
    {
        var amenities =
            _driver.FindElements(_amenityItems);

        return amenities.Any(amenity =>
            amenity.Text.Trim().Equals(
                "Parking",
                StringComparison.OrdinalIgnoreCase));
    }
}