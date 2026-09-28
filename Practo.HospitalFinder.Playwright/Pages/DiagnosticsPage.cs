using Microsoft.Playwright;

namespace Practo.HospitalFinder.Playwright.Pages;

public class DiagnosticsPage
{
    private readonly IPage _page;

    public DiagnosticsPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToDiagnosticsAsync()
    {
        await _page.GotoAsync(
            "https://www.practo.com/tests",
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });
    }

    public async Task<List<string>> GetTopCitiesAsync()
    {
        var topCitiesHeading =
            _page.GetByText(
                "TOP CITIES",
                new() { Exact = true });

        await topCitiesHeading.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        var topCitiesList =
            topCitiesHeading.Locator(
                "xpath=following::ul[1]");

        await topCitiesList.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        var cityItems =
            topCitiesList.Locator("li");

        int count =
            await cityItems.CountAsync();

        var cities =
            new List<string>();

        for (int i = 0; i < count; i++)
        {
            var cityName =
                cityItems
                    .Nth(i)
                    .Locator(
                        ".u-margint--standard.o-f-color--primary");

            if (await cityName.CountAsync() == 0)
            {
                continue;
            }

            string name =
                (await cityName.InnerTextAsync()).Trim();

            if (!string.IsNullOrWhiteSpace(name))
            {
                cities.Add(name);
            }
        }

        return cities;
    }
}