using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace Practo.HospitalFinder.Playwright;

[TestFixture]
public class PlaywrightSmokeTests : PageTest
{
    [Test]
    public async Task PlaywrightCanOpenPractoHomepage()
    {
        await Page.GotoAsync("https://www.practo.com/");

        await Expect(Page).ToHaveTitleAsync(
            new Regex("Practo", RegexOptions.IgnoreCase));
    }
}