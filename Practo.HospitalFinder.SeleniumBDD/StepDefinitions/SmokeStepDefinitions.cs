using NUnit.Framework;
using Reqnroll;

namespace Practo.HospitalFinder.SeleniumBDD.StepDefinitions;

[Binding]
public class SmokeStepDefinitions
{
    private bool _frameworkConfigured;
    private bool _scenarioExecuted;

    [Given("the BDD framework is configured")]
    public void GivenTheBddFrameworkIsConfigured()
    {
        _frameworkConfigured = true;
    }

    [When("the smoke scenario is executed")]
    public void WhenTheSmokeScenarioIsExecuted()
    {
        _scenarioExecuted = true;
    }

    [Then("the scenario should pass")]
    public void ThenTheScenarioShouldPass()
    {
        Assert.That(_frameworkConfigured, Is.True);
        Assert.That(_scenarioExecuted, Is.True);
    }
}