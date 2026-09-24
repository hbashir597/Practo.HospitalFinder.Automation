using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Practo.HospitalFinder.Playwright.Pages;
using System.Text.RegularExpressions;

namespace Practo.HospitalFinder.Playwright.Tests;

[TestFixture]
public class CorporateWellnessTests : PageTest
{
    [Test]
    public async Task CorporateWellnessPageLoads()
    {
        var corporateWellnessPage =
            new CorporateWellnessPage(Page);

        await corporateWellnessPage.NavigateAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex("/plus/corporate", RegexOptions.IgnoreCase));

        await Expect(
            Page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Schedule a Demo", Exact = true })
                .First)
            .ToBeVisibleAsync();
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

        await corporateWellnessPage
            .FillInvalidContactDetailsAsync(
                contactNumber,
                email);

        await Expect(
            corporateWellnessPage.InvalidContactNumber)
            .ToBeVisibleAsync();

        await Expect(
            corporateWellnessPage.InvalidOfficialEmail)
            .ToBeVisibleAsync();

        await Expect(
            corporateWellnessPage.ScheduleDemoButton)
            .ToBeDisabledAsync();

        Console.WriteLine(
            $"Invalid contact number rejected: {contactNumber}");

        Console.WriteLine(
            $"Invalid email rejected: {email}");

        Console.WriteLine(
            "Schedule a demo button is disabled.");
    }
}