using NUnit.Framework;
using OpenQA.Selenium;
using Practo.HospitalFinder.SeleniumBDD.Models;
using Practo.HospitalFinder.SeleniumBDD.Pages;
using Reqnroll;

namespace Practo.HospitalFinder.SeleniumBDD.StepDefinitions;

[Binding]
public class HospitalSearchStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IWebDriver _driver;
    private readonly HospitalSearchPage _hospitalSearchPage;
    private readonly HospitalDetailsPage _hospitalDetailsPage;

    private List<HospitalResult> _hospitalResults = new();
    private List<HospitalResult> _candidateHospitals = new();

    public HospitalSearchStepDefinitions(
        ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;

        _driver =
            _scenarioContext.Get<IWebDriver>("WebDriver");

        _hospitalSearchPage =
            new HospitalSearchPage(_driver);

        _hospitalDetailsPage =
            new HospitalDetailsPage(_driver);
    }

    [Given("I am viewing hospitals in Bangalore")]
    public void GivenIAmViewingHospitalsInBangalore()
    {
        _hospitalSearchPage
            .NavigateToBangaloreHospitals();

        Assert.That(
            _hospitalSearchPage.GetHospitalCards().Count,
            Is.GreaterThan(0),
            "Expected hospital cards to be displayed.");
    }

    [When("I search the available hospitals")]
    public void WhenISearchTheAvailableHospitals()
    {
        _hospitalResults =
            _hospitalSearchPage.GetHospitalResults();

        Assert.That(
            _hospitalResults,
            Is.Not.Empty,
            "Expected hospital information to be extracted.");

        Console.WriteLine(
            $"Hospitals extracted: {_hospitalResults.Count}");

        foreach (var hospital in _hospitalResults)
        {
            Console.WriteLine(
                $"{hospital.Name} | " +
                $"{hospital.Location} | " +
                $"Rating: {hospital.Rating?.ToString() ?? "N/A"} | " +
                $"Open 24x7: {hospital.IsOpen24x7}");
        }
    }

    [Then("I should find hospitals that are open {int} hours")]
    public void ThenIShouldFindHospitalsThatAreOpenHours(
        int hours)
    {
        Assert.That(
            hours,
            Is.EqualTo(24),
            "This scenario expects the 24-hour requirement.");

        _candidateHospitals =
            _hospitalSearchPage
                .GetCandidateHospitals(_hospitalResults);

        Assert.That(
            _candidateHospitals,
            Is.Not.Empty,
            "Expected at least one hospital to be open 24x7 " +
            "with a rating greater than 3.5.");

        Assert.That(
            _candidateHospitals.All(
                hospital => hospital.IsOpen24x7),
            Is.True,
            "Expected every candidate hospital to be open 24x7.");

        Console.WriteLine(
            $"Candidate hospitals: {_candidateHospitals.Count}");
    }

    [Then("the hospitals should have a rating greater than {float}")]
    public void ThenTheHospitalsShouldHaveARatingGreaterThan(
        decimal minimumRating)
    {
        Assert.That(
            _candidateHospitals,
            Is.Not.Empty,
            "Expected candidate hospitals before checking ratings.");

        Assert.That(
            _candidateHospitals.All(
                hospital =>
                    hospital.Rating.HasValue &&
                    hospital.Rating.Value >
                    (double)minimumRating),
            Is.True,
            $"Expected every candidate hospital to have " +
            $"a rating greater than {minimumRating}.");

        foreach (var hospital in _candidateHospitals)
        {
            Console.WriteLine(
                $"Candidate: {hospital.Name} | " +
                $"Rating: {hospital.Rating} | " +
                $"Open 24x7: {hospital.IsOpen24x7}");
        }
    }

    [Then("the hospitals should have parking available")]
    public void ThenTheHospitalsShouldHaveParkingAvailable()
    {
        var matchingHospitals =
            new List<HospitalResult>();

        foreach (var hospital in _candidateHospitals)
        {
            Console.WriteLine(
                $"Checking parking: {hospital.Name}");

            string? originalWindow = null;

            try
            {
                originalWindow =
                    _hospitalSearchPage
                        .OpenHospitalDetails(hospital);

                _hospitalDetailsPage
                    .ExpandHospitalInformation();

                var hasParking =
                    _hospitalDetailsPage.HasParking();

                Console.WriteLine(
                    $"Parking available: {hasParking}");

                if (hasParking)
                {
                    matchingHospitals.Add(hospital);
                }
            }
            finally
            {
                if (originalWindow != null &&
                    _driver.WindowHandles.Count > 1)
                {
                    _hospitalSearchPage
                        .CloseHospitalDetails(
                            originalWindow);
                }
            }
        }

        Assert.That(
            matchingHospitals,
            Is.Not.Empty,
            "Expected at least one hospital to satisfy " +
            "all required criteria.");

        Console.WriteLine(
            $"Hospitals matching all criteria: " +
            $"{matchingHospitals.Count}");

        foreach (var hospital in matchingHospitals)
        {
            Console.WriteLine(
                $"MATCH: {hospital.Name} | " +
                $"{hospital.Location} | " +
                $"Rating: {hospital.Rating}");
        }
    }
}