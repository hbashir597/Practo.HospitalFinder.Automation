using Microsoft.Playwright;

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
        await _page.GotoAsync("https://www.practo.com/bangalore/hospitals");
    }

    public async Task<int> GetHospitalCountAsync()
    {
        return await _page.Locator(".c-estb-card").CountAsync();
    }
}