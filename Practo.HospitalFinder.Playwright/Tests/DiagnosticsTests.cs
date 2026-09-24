using Allure.NUnit;
using Practo.HospitalFinder.Playwright.Pages;
using Practo.HospitalFinder.Playwright.Support;
using System.Text.RegularExpressions;

namespace Practo.HospitalFinder.Playwright.Tests;

[TestFixture]
[AllureNUnit]
public class DiagnosticsTests : BaseTest
{
    [Test]
    public async Task DiagnosticsPageLoads()
    {
        var diagnosticsPage =
            new DiagnosticsPage(Page);

        await diagnosticsPage.NavigateToDiagnosticsAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex("/tests", RegexOptions.IgnoreCase));
    }

    [Test]
    public async Task DiagnosticsPageDisplaysTopCities()
    {
        var diagnosticsPage =
            new DiagnosticsPage(Page);

        await diagnosticsPage.NavigateToDiagnosticsAsync();

        List<string> topCities =
            await diagnosticsPage.GetTopCitiesAsync();

        Assert.That(
            topCities,
            Is.Not.Empty,
            "Expected at least one city in the Top Cities section.");

        Console.WriteLine(
            "========== TOP CITIES ==========");

        foreach (string city in topCities)
        {
            Console.WriteLine(city);
        }

        Console.WriteLine(
            $"Total Top Cities: {topCities.Count}");

        Console.WriteLine(
            "================================");
    }
}