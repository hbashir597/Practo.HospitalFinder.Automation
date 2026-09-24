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
        if (_scenarioContext.TryGetValue(
                "WebDriver",
                out IWebDriver? driver))
        {
            driver?.Quit();
        }
    }
}