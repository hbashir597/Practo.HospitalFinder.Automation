using Allure.Net.Commons;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;

namespace Practo.HospitalFinder.Playwright.Support;

public class BaseTest : PageTest
{
    [TearDown]
    public async Task CaptureFailureEvidenceAsync()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status
            != TestStatus.Failed)
        {
            return;
        }

        var projectDirectory =
            Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    ".."));

        var screenshotDirectory =
            Path.Combine(
                projectDirectory,
                "TestResults",
                "Screenshots");

        Directory.CreateDirectory(
            screenshotDirectory);

        var safeTestName =
            string.Concat(
                TestContext.CurrentContext.Test.Name
                    .Select(character =>
                        Path.GetInvalidFileNameChars()
                            .Contains(character)
                                ? '_'
                                : character));

        var screenshotPath =
            Path.Combine(
                screenshotDirectory,
                $"{safeTestName}_" +
                $"{DateTime.Now:yyyyMMdd_HHmmss}.png");

        await Page.ScreenshotAsync(
            new()
            {
                Path = screenshotPath,
                FullPage = true
            });

        AllureApi.AddAttachment(
            "Failure Screenshot",
            "image/png",
            screenshotPath);

        TestContext.Out.WriteLine(
            $"Failure screenshot saved: {screenshotPath}");
    }
}