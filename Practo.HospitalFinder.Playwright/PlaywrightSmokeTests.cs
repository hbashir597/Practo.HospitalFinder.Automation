using Practo.HospitalFinder.Playwright.Support;
using System.Text.RegularExpressions;

namespace Practo.HospitalFinder.Playwright;

[TestFixture]
public class PlaywrightSmokeTests : BaseTest
{
    [Test]
    public async Task PlaywrightCanOpenPractoHomepage()
    {
        await Page.GotoAsync(
            "https://www.practo.com/");

        await DismissConsentPopupIfPresentAsync();

        await Expect(Page).ToHaveTitleAsync(
            new Regex(
                "Practo",
                RegexOptions.IgnoreCase));

        TestContext.Out.WriteLine(
            "Practo homepage loaded successfully.");
    }
}