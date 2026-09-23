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
}