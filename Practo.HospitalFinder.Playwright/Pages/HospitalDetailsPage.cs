using Microsoft.Playwright;

namespace Practo.HospitalFinder.Playwright.Pages;

public class HospitalDetailsPage
{
    private readonly IPage _page;

    private const string DoNotConsentSelector =
        "button[aria-label='Do not consent']";

    private const string ReadMoreInfoSelector =
        "[data-qa-id='read_more_info']";

    private const string AmenityItemSelector =
        "[data-qa-id='amenity_item']";

    public HospitalDetailsPage(IPage page)
    {
        _page = page;
    }

    public async Task DismissConsentPopupIfPresentAsync()
    {
        var doNotConsentButton =
            _page.Locator(DoNotConsentSelector);

        try
        {
            await doNotConsentButton
                .First
                .WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 3000
                    });

            await doNotConsentButton
                .First
                .ClickAsync();

            Console.WriteLine(
                "Consent popup dismissed.");
        }
        catch (TimeoutException)
        {
            Console.WriteLine(
                "Consent popup not displayed.");
        }
    }

    public async Task ExpandHospitalInformationAsync()
    {
        await DismissConsentPopupIfPresentAsync();

        var readMoreInfo =
            _page.Locator(
                ReadMoreInfoSelector);

        await readMoreInfo
            .First
            .WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

        await readMoreInfo
            .First
            .ScrollIntoViewIfNeededAsync();

        await readMoreInfo
            .First
            .ClickAsync();

        await _page
            .Locator(AmenityItemSelector)
            .First
            .WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

        Console.WriteLine(
            "Hospital information expanded.");
    }

    public async Task<bool> HasParkingAsync()
    {
        await _page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        string title =
            await _page.TitleAsync();

        Console.WriteLine(
            $"Hospital details URL: {_page.Url}");

        Console.WriteLine(
            $"Hospital details title: {title}");

        if (title.Contains(
                "Challenge Validation",
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Practo returned its Challenge Validation page " +
                "instead of the hospital details page.");
        }

        await ExpandHospitalInformationAsync();

        var amenities =
            _page.Locator(
                AmenityItemSelector);

        int amenityCount =
            await amenities.CountAsync();

        Console.WriteLine(
            $"Amenities found: {amenityCount}");

        for (int i = 0; i < amenityCount; i++)
        {
            var amenity =
                amenities.Nth(i);

            string amenityText =
                (await amenity.InnerTextAsync())
                .Trim();

            if (amenityText.Equals(
                    "Parking",
                    StringComparison.OrdinalIgnoreCase))
            {
                await amenity
                    .ScrollIntoViewIfNeededAsync();

                Console.WriteLine(
                    "Parking amenity found: True");

                return true;
            }
        }

        Console.WriteLine(
            "Parking amenity found: False");

        return false;
    }
}