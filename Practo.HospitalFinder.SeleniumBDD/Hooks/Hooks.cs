using Allure.Net.Commons;
using OpenQA.Selenium;
using Practo.HospitalFinder.SeleniumBDD.Drivers;
using Practo.HospitalFinder.SeleniumBDD.Logging;
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

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Logger.Initialise();

        Logger.Info(
            "BDD test run started");
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        Logger.Info(
            $"Starting scenario: " +
            $"{_scenarioContext.ScenarioInfo.Title}");
    }

    [BeforeScenario("@ui")]
    public void BeforeUiScenario()
    {
        Logger.Info(
            "Creating Selenium WebDriver");

        IWebDriver driver =
            DriverFactory.CreateDriver();

        _scenarioContext["WebDriver"] = driver;

        Logger.Info(
            "Selenium WebDriver created successfully");
    }

    [AfterScenario("@ui")]
    public void AfterUiScenario()
    {
        if (!_scenarioContext.TryGetValue(
                "WebDriver",
                out IWebDriver? driver) ||
            driver is null)
        {
            Logger.Info(
                "No WebDriver was available for cleanup");

            return;
        }

        try
        {
            if (_scenarioContext.TestError is not null)
            {
                Logger.Error(
                    $"Scenario failed: " +
                    $"{_scenarioContext.ScenarioInfo.Title}",
                    _scenarioContext.TestError);

                CaptureFailureScreenshot(driver);
            }
        }
        finally
        {
            driver.Quit();

            Logger.Info(
                "Selenium WebDriver closed");
        }
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenarioContext.TestError is null)
        {
            Logger.Info(
                $"Scenario passed: " +
                $"{_scenarioContext.ScenarioInfo.Title}");
        }
        else
        {
            Logger.Error(
                $"Scenario completed with failure: " +
                $"{_scenarioContext.ScenarioInfo.Title}",
                _scenarioContext.TestError);
        }
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Logger.Info(
            "BDD test run finished");

        Logger.Close();
    }

    private void CaptureFailureScreenshot(
        IWebDriver driver)
    {
        if (driver is not ITakesScreenshot screenshotDriver)
        {
            Logger.Info(
                "WebDriver does not support screenshots");

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

        Logger.Info(
            $"Failure screenshot saved: " +
            $"{screenshotPath}");

        Console.WriteLine(
            $"Failure screenshot saved: " +
            $"{screenshotPath}");
    }
}