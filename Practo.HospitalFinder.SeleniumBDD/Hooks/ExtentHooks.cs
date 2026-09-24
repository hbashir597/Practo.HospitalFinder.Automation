using AventStack.ExtentReports;
using Practo.HospitalFinder.SeleniumBDD.Reporting;
using Reqnroll;

namespace Practo.HospitalFinder.SeleniumBDD.Hooks;

[Binding]
public sealed class ExtentHooks
{
    private readonly ScenarioContext _scenarioContext;
    private ExtentTest? _test;

    public ExtentHooks(
        ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario(Order = -50)]
    public void BeforeScenario()
    {
        _test =
            ExtentReport.Instance.CreateTest(
                _scenarioContext.ScenarioInfo.Title);

        foreach (var tag in
                 _scenarioContext.ScenarioInfo.CombinedTags)
        {
            _test.AssignCategory(tag);
        }
    }

    [AfterStep]
    public void AfterStep()
    {
        var stepText =
            _scenarioContext.StepContext
                .StepInfo.Text;

        _test?.Info(stepText);
    }

    [AfterScenario(Order = 200)]
    public void AfterScenario()
    {
        if (_scenarioContext.TestError is null)
        {
            _test?.Pass("Scenario passed");
        }
        else
        {
            _test?.Fail(
                _scenarioContext.TestError);
        }

        ExtentReport.Instance.Flush();
    }
}