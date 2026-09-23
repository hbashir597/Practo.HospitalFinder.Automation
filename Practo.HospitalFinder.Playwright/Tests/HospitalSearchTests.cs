using Microsoft.Playwright.NUnit;
using Practo.HospitalFinder.Playwright.Pages;
using System.Text.RegularExpressions;

namespace Practo.HospitalFinder.Playwright.Tests;

[TestFixture]
public class HospitalSearchTests : PageTest
{
    [Test]
    public async Task BangaloreHospitalSearchPageLoads()
    {
        var hospitalSearchPage = new HospitalSearchPage(Page);

        await hospitalSearchPage.NavigateToBangaloreHospitalsAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex("bangalore/hospitals", RegexOptions.IgnoreCase));
    }

    [Test]
    public async Task BangaloreHospitalSearchDisplaysHospitalCards()
    {
        var hospitalSearchPage = new HospitalSearchPage(Page);

        await hospitalSearchPage.NavigateToBangaloreHospitalsAsync();

        int hospitalCount =
            await hospitalSearchPage.GetHospitalCountAsync();

        Console.WriteLine(
            $"Hospital cards found on page: {hospitalCount}");

        Assert.That(
            hospitalCount,
            Is.GreaterThan(0),
            "Expected at least one hospital card to be displayed.");
    }

    [Test]
    public async Task BangaloreHospitalSearchExtractsHospitalInformation()
    {
        var hospitalSearchPage = new HospitalSearchPage(Page);

        await hospitalSearchPage.NavigateToBangaloreHospitalsAsync();

        var hospitals =
            await hospitalSearchPage.GetHospitalResultsAsync();

        Assert.That(
            hospitals,
            Is.Not.Empty,
            "Expected hospital information to be extracted.");

        foreach (var hospital in hospitals)
        {
            Console.WriteLine(
                $"Name: {hospital.Name} | " +
                $"Rating: {hospital.Rating} | " +
                $"Open 24x7: {hospital.IsOpen24x7} | " +
                $"URL: {hospital.DetailsUrl}");
        }
    }

    [Test]
    public async Task BangaloreHospitalSearchFiltersCandidateHospitals()
    {
        var hospitalSearchPage = new HospitalSearchPage(Page);

        await hospitalSearchPage.NavigateToBangaloreHospitalsAsync();

        var candidates =
            await hospitalSearchPage.GetCandidateHospitalsAsync();

        Assert.That(
            candidates,
            Is.Not.Empty,
            "Expected at least one hospital to meet the rating and 24x7 criteria.");

        foreach (var hospital in candidates)
        {
            Console.WriteLine(
                $"Candidate: {hospital.Name} | " +
                $"Rating: {hospital.Rating} | " +
                $"Open 24x7: {hospital.IsOpen24x7}");

            Assert.That(
                hospital.IsOpen24x7,
                Is.True);

            Assert.That(
                hospital.Rating,
                Is.GreaterThan(3.5));
        }

        Console.WriteLine(
            $"Total candidate hospitals: {candidates.Count}");
    }
}