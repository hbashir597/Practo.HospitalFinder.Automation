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
}