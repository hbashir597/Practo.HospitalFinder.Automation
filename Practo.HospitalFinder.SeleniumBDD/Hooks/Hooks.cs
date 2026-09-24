using Allure.Net.Commons;
using OpenQA.Selenium;
using Practo.HospitalFinder.SeleniumBDD.Drivers;
using Reqnroll;

namespace Practo.HospitalFinder.SeleniumBDD.Hooks;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _scenarioContext;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario("@ui")]
    public void BeforeUiScenario()
    {
        IWebDriver driver =
            DriverFactory.CreateDriver();

        _scenarioContext["WebDriver"] = driver;
    }

    [AfterScenario("@ui")]
    public void AfterUiScenario()
    {
        if (!_scenarioContext.TryGetValue(
                "WebDriver",
                out IWebDriver? driver) ||
            driver is null)
        {
            return;
        }

        try
        {
            if (_scenarioContext.TestError is not null)
            {
                CaptureFailureScreenshot(driver);
            }
        }
        finally
        {
            driver.Quit();
        }
    }

    private void CaptureFailureScreenshot(
        IWebDriver driver)
    {
        if (driver is not ITakesScreenshot screenshotDriver)
        {
            return;
        }

        var projectDirectory =
            Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    ".."));

        var screenshotDirectory =
            Path.Combine(
                projectDirectory,
                "TestResults",
                "Screenshots");

        Directory.CreateDirectory(
            screenshotDirectory);

        var safeScenarioName =
            string.Concat(
                _scenarioContext.ScenarioInfo.Title
                    .Select(character =>
                        Path.GetInvalidFileNameChars()
                            .Contains(character)
                                ? '_'
                                : character));

        var screenshotPath =
            Path.Combine(
                screenshotDirectory,
                $"{safeScenarioName}_" +
                $"{DateTime.Now:yyyyMMdd_HHmmss}.png");

        var screenshot =
            screenshotDriver.GetScreenshot();

        screenshot.SaveAsFile(
            screenshotPath);

        AllureApi.AddAttachment(
            "Failure Screenshot",
            "image/png",
            screenshot.AsByteArray);

        Console.WriteLine(
            $"Failure screenshot saved: " +
            $"{screenshotPath}");
    }
}