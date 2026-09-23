using Microsoft.Playwright.NUnit;
using Practo.HospitalFinder.Playwright.Models;
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
                $"Location: {hospital.Location} | " +
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
                $"Location: {hospital.Location} | " +
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

    [Test]
    public async Task HospitalDetailsDetectsParkingAmenity()
    {
        await Page.GotoAsync(
            "https://www.practo.com/bangalore/hospital/columbia-asia-hospital1-whitefield?referrer=hospital_listing");

        var hospitalDetailsPage =
            new HospitalDetailsPage(Page);

        bool hasParking =
            await hospitalDetailsPage.HasParkingAsync();

        Console.WriteLine(
            $"Parking amenity found: {hasParking}");

        Assert.That(
            hasParking,
            Is.True,
            "Expected Parking to be listed in the hospital amenities.");
    }

    [Test]
    public async Task BangaloreHospitalSearchFindsHospitalsMatchingAllCriteria()
    {
        var hospitalSearchPage =
            new HospitalSearchPage(Page);

        await hospitalSearchPage.NavigateToBangaloreHospitalsAsync();

        var candidates =
            await hospitalSearchPage.GetCandidateHospitalsAsync();

        Assert.That(
            candidates,
            Is.Not.Empty,
            "Expected at least one hospital to meet the rating and 24x7 criteria.");

        Console.WriteLine(
            $"Candidate hospitals found: {candidates.Count}");

        var matchingHospitals =
            new List<HospitalResult>();

        for (int i = 0; i < candidates.Count; i++)
        {
            var hospital = candidates[i];

            Console.WriteLine();

            Console.WriteLine(
                $"Checking candidate {i + 1}/{candidates.Count}: " +
                $"{hospital.Name} - {hospital.Location}");

            var detailsPage =
                await hospitalSearchPage
                    .OpenHospitalDetailsInNewTabAsync(hospital);

            try
            {
                var hospitalDetailsPage =
                    new HospitalDetailsPage(detailsPage);

                bool hasParking =
                    await hospitalDetailsPage.HasParkingAsync();

                Console.WriteLine(
                    $"Location: {hospital.Location} | " +
                    $"Rating: {hospital.Rating} | " +
                    $"Open 24x7: {hospital.IsOpen24x7} | " +
                    $"Parking: {hasParking}");

                if (hasParking)
                {
                    matchingHospitals.Add(hospital);

                    Console.WriteLine("Result: MATCH");
                }
                else
                {
                    Console.WriteLine("Result: REJECT");
                }
            }
            finally
            {
                await detailsPage.CloseAsync();
            }
        }

        Assert.That(
            matchingHospitals,
            Is.Not.Empty,
            "Expected at least one hospital to meet all three criteria.");

        Console.WriteLine();

        Console.WriteLine(
            "========== HOSPITALS MATCHING ALL CRITERIA ==========");

        foreach (var hospital in matchingHospitals)
        {
            Console.WriteLine(
                $"{hospital.Name} | " +
                $"Location: {hospital.Location} | " +
                $"Rating: {hospital.Rating} | " +
                "Open 24x7: Yes | " +
                "Parking: Yes");
        }

        Console.WriteLine(
            $"Total matching hospitals: {matchingHospitals.Count}");

        Console.WriteLine(
            "====================================================");
    }
}