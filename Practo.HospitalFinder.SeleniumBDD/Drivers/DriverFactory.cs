using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Practo.HospitalFinder.SeleniumBDD.Drivers;

public static class DriverFactory
{
    public static IWebDriver CreateDriver()
    {
        var options = new ChromeOptions();

        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-dev-shm-usage");

        var useRemoteDriver =
            Environment.GetEnvironmentVariable("SELENIUM_REMOTE");

        if (string.Equals(
                useRemoteDriver,
                "true",
                StringComparison.OrdinalIgnoreCase))
        {
            var gridUrl =
                Environment.GetEnvironmentVariable("SELENIUM_GRID_URL")
                ?? "http://localhost:4444";

            return new RemoteWebDriver(
                new Uri(gridUrl),
                options);
        }

        return new ChromeDriver(options);
    }
}