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
            "https://www.practo.com/plus/corporate");
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

        await name.FillAsync("Test");

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

        await officialEmail.PressAsync("Tab");
    }

    public ILocator InvalidContactNumber =>
        _page.Locator(
            "input[placeholder='Contact Number'].corporate-form__input--error:visible");

    public ILocator InvalidOfficialEmail =>
        _page.Locator(
            "input[placeholder='Official Email ID'].corporate-form__input--error:visible");

    public ILocator ScheduleDemoButton =>
        _page.Locator(
            "button[type='submit']:visible")
            .Filter(new() { HasText = "Schedule a demo" });
}