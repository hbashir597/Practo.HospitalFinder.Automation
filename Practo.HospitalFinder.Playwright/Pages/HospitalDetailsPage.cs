using Microsoft.Playwright;

namespace Practo.HospitalFinder.Playwright.Pages;

public class HospitalDetailsPage
{
    private readonly IPage _page;

    public HospitalDetailsPage(IPage page)
    {
        _page = page;
    }

    public async Task<bool> HasParkingAsync()
    {
        await _page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        var readMoreInfo =
            _page.GetByText(
                "Read more info",
                new() { Exact = true });

        if (await readMoreInfo.CountAsync() > 0 &&
            await readMoreInfo.First.IsVisibleAsync())
        {
            await readMoreInfo.First.ClickAsync();
        }

        var amenitiesList =
            _page.Locator("[data-qa-id='amenities_list']");

        if (await amenitiesList.CountAsync() == 0)
        {
            return false;
        }

        var parkingAmenity =
            amenitiesList.GetByText(
                "Parking",
                new() { Exact = true });

        return await parkingAmenity.CountAsync() > 0;
    }
}