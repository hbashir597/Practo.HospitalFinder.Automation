using Allure.NUnit;
using Practo.HospitalFinder.Playwright.Pages;
using Practo.HospitalFinder.Playwright.Support;
using System.Text.RegularExpressions;

namespace Practo.HospitalFinder.Playwright.Tests;

[TestFixture]
[AllureNUnit]
public class CorporateWellnessTests : BaseTest
{
    [Test]
    public async Task CorporateWellnessPageLoads()
    {
        var corporateWellnessPage =
            new CorporateWellnessPage(Page);

        await corporateWellnessPage.NavigateAsync();

        await DismissConsentPopupIfPresentAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex(
                "/plus/corporate",
                RegexOptions.IgnoreCase));

        await Expect(
            corporateWellnessPage.ScheduleDemoHeading)
            .ToBeVisibleAsync();

        Console.WriteLine(
            "Corporate Wellness page loaded successfully.");
    }

    [TestCase("123", "invalid-email")]
    [TestCase("abcdef", "123445")]
    public async Task CorporateWellnessFormRejectsInvalidContactDetails(
        string contactNumber,
        string email)
    {
        var corporateWellnessPage =
            new CorporateWellnessPage(Page);

        await corporateWellnessPage.NavigateAsync();

        await DismissConsentPopupIfPresentAsync();

        await corporateWellnessPage
            .FillInvalidContactDetailsAsync(
                contactNumber,
                email);

        await Expect(
            corporateWellnessPage.ScheduleDemoButton)
            .ToBeDisabledAsync();

        Console.WriteLine(
            $"Invalid contact number entered: {contactNumber}");

        Console.WriteLine(
            $"Invalid email entered: {email}");

        Console.WriteLine(
            "Schedule a demo button remained disabled.");

        Console.WriteLine(
            "Invalid contact details were rejected by the form.");
    }
}