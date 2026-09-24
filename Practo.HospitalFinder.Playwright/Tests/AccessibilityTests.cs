using Allure.NUnit;
using Deque.AxeCore.Playwright;
using Practo.HospitalFinder.Playwright.Support;

namespace Practo.HospitalFinder.Playwright.Tests;

[TestFixture]
[AllureNUnit]
public class AccessibilityTests : BaseTest
{
    [Test]
    public async Task HomepageAccessibilityScan()
    {
        await Page.GotoAsync(
            "https://www.practo.com/");

        var results =
            await Page.RunAxe();

        TestContext.Out.WriteLine(
            $"Accessibility violations: " +
            $"{results.Violations.Length}");

        foreach (var violation in results.Violations)
        {
            TestContext.Out.WriteLine(
                $"Rule: {violation.Id}");

            TestContext.Out.WriteLine(
                $"Impact: {violation.Impact}");

            TestContext.Out.WriteLine(
                $"Description: {violation.Description}");

            TestContext.Out.WriteLine(
                $"Help: {violation.Help}");

            TestContext.Out.WriteLine(
                "--------------------------------");
        }

        Assert.That(
            results,
            Is.Not.Null);
    }

    [Test]
    public async Task LocationSearchIsReachableByKeyboard()
    {
        await Page.GotoAsync(
            "https://www.practo.com/");

        const int maximumTabPresses = 15;

        var locationReached = false;

        for (var tabNumber = 1;
             tabNumber <= maximumTabPresses;
             tabNumber++)
        {
            await Page.Keyboard.PressAsync("Tab");

            var focusedElement =
                await Page.EvaluateAsync<string>(
                    """
                    () => {
                        const element =
                            document.activeElement;

                        if (!element) {
                            return "No focused element";
                        }

                        return [
                            element.tagName,
                            element.getAttribute("href") ?? "",
                            element.getAttribute("placeholder") ?? "",
                            element.textContent?.trim() ?? ""
                        ]
                        .filter(Boolean)
                        .join(" | ");
                    }
                    """);

            TestContext.Out.WriteLine(
                $"Tab {tabNumber}: " +
                $"{focusedElement}");

            var locationIsFocused =
                await Page.EvaluateAsync<bool>(
                    """
                    () => {
                        const element =
                            document.activeElement;

                        return element?.getAttribute(
                            "data-qa-id") ===
                            "omni-searchbox-locality";
                    }
                    """);

            if (locationIsFocused)
            {
                locationReached = true;

                TestContext.Out.WriteLine(
                    "Location field reached " +
                    "using keyboard.");

                break;
            }
        }

        Assert.That(
            locationReached,
            Is.True,
            "The location search field could not " +
            "be reached using keyboard navigation.");
    }
}