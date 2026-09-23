using Microsoft.Playwright;
using Practo.HospitalFinder.Playwright.Models;
using System.Globalization;

namespace Practo.HospitalFinder.Playwright.Pages;

public class HospitalSearchPage
{
    private readonly IPage _page;

    public HospitalSearchPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToBangaloreHospitalsAsync()
    {
        await _page.GotoAsync(
            "https://www.practo.com/bangalore/hospitals");
    }

    public async Task<int> GetHospitalCountAsync()
    {
        return await _page
            .Locator(".c-estb-card")
            .CountAsync();
    }

    public async Task<List<HospitalResult>> GetHospitalResultsAsync()
    {
        var results = new List<HospitalResult>();

        var hospitalCards =
            _page.Locator(".c-estb-card");

        int count =
            await hospitalCards.CountAsync();

        for (int i = 0; i < count; i++)
        {
            var card =
                hospitalCards.Nth(i);

            // Hospital name
            var nameLocator =
                card.Locator("h2.line-1");

            if (await nameLocator.CountAsync() == 0)
            {
                continue;
            }

            string name =
                (await nameLocator.InnerTextAsync()).Trim();

            // Hospital locality
            string location = string.Empty;

            var locationLocator =
                card.Locator(".c-locality-info > span").First;

            if (await locationLocator.CountAsync() > 0)
            {
                location =
                    (await locationLocator.InnerTextAsync()).Trim();
            }

            // Open 24x7 status
            bool isOpen24x7 =
                await card
                    .GetByText(
                        "Open 24x7",
                        new() { Exact = true })
                    .CountAsync() > 0;

            // Hospital rating
            double? rating = null;

            var ratingLocator =
                card.Locator(
                    ".c-feedback .text-1 .u-bold");

            if (await ratingLocator.CountAsync() > 0)
            {
                string ratingText =
                    (await ratingLocator
                        .First
                        .InnerTextAsync())
                    .Trim();

                if (double.TryParse(
                    ratingText,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double parsedRating))
                {
                    rating = parsedRating;
                }
            }

            // Hospital details URL
            string detailsUrl =
                string.Empty;

            var detailsLink =
                card
                    .Locator("a:has(h2.line-1)")
                    .First;

            if (await detailsLink.CountAsync() > 0)
            {
                detailsUrl =
                    await detailsLink
                        .GetAttributeAsync("href")
                    ?? string.Empty;
            }

            // Store extracted hospital information
            results.Add(
                new HospitalResult
                {
                    Name = name,
                    Location = location,
                    Rating = rating,
                    IsOpen24x7 = isOpen24x7,
                    DetailsUrl = detailsUrl
                });
        }

        return results;
    }

    public async Task<List<HospitalResult>> GetCandidateHospitalsAsync()
    {
        var hospitals =
            await GetHospitalResultsAsync();

        return hospitals
            .Where(hospital =>
                hospital.IsOpen24x7 &&
                hospital.Rating.HasValue &&
                hospital.Rating.Value > 3.5)
            .ToList();
    }

    public async Task<IPage> OpenHospitalDetailsInNewTabAsync(
        HospitalResult hospital)
    {
        var hospitalCards =
            _page.Locator(".c-estb-card");

        int count =
            await hospitalCards.CountAsync();

        for (int i = 0; i < count; i++)
        {
            var card =
                hospitalCards.Nth(i);

            var detailsLink =
                card
                    .Locator("a:has(h2.line-1)")
                    .First;

            if (await detailsLink.CountAsync() == 0)
            {
                continue;
            }

            string linkUrl =
                await detailsLink.GetAttributeAsync("href")
                ?? string.Empty;

            if (linkUrl != hospital.DetailsUrl)
            {
                continue;
            }

            var detailsPage =
                await _page.Context.RunAndWaitForPageAsync(
                    async () =>
                    {
                        await detailsLink.ClickAsync();
                    });

            await detailsPage.WaitForLoadStateAsync(
                LoadState.DOMContentLoaded);

            return detailsPage;
        }

        throw new InvalidOperationException(
            $"Could not find the hospital card for " +
            $"'{hospital.Name}' in '{hospital.Location}'.");
    }
}