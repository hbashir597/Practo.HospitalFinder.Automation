using Microsoft.Playwright;

namespace Practo.HospitalFinder.Playwright.Pages;

public class CorporateWellnessPage
{
    private readonly IPage _page;

    public CorporateWellnessPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync(
            "https://www.practo.com/plus/corporate",
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
    }

    public async Task FillInvalidContactDetailsAsync(
        string contactNumber,
        string email)
    {
        var name =
            _page.Locator(
                "input[placeholder='Name']:visible");

        var organisationName =
            _page.Locator(
                "input[placeholder='Organization Name']:visible");

        var contactNumberInput =
            _page.Locator(
                "input[placeholder='Contact Number']:visible");

        var officialEmail =
            _page.Locator(
                "input[placeholder='Official Email ID']:visible");

        var organisationSize =
            _page.Locator(
                "select#organizationSize:visible");

        var interestedIn =
            _page.Locator(
                "select#interestedIn:visible");

        await name.FillAsync(
            "Test");

        await organisationName.FillAsync(
            "Test Organisation");

        await contactNumberInput.FillAsync(
            contactNumber);

        await officialEmail.FillAsync(
            email);

        await organisationSize.SelectOptionAsync(
            new SelectOptionValue
            {
                Label = "<500"
            });

        await interestedIn.SelectOptionAsync(
            new SelectOptionValue
            {
                Label = "Taking a demo"
            });

        await officialEmail.PressAsync(
            "Tab");
    }

    public ILocator ScheduleDemoHeading =>
        _page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Schedule a Demo",
                Exact = true
            })
            .First;

    public ILocator ScheduleDemoButton =>
        _page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Schedule a demo",
                Exact = true
            });
}