using Allure.NUnit;
using Deque.AxeCore.Playwright;
using Practo.HospitalFinder.Playwright.Pages;
using Practo.HospitalFinder.Playwright.Support;

namespace Practo.HospitalFinder.Playwright.Tests;

[TestFixture]
[AllureNUnit]
public class AccessibilityTests : BaseTest
{
    [Test]
    public async Task DiagnosticsPageAccessibilityScan()
    {
        var diagnosticsPage =
            new DiagnosticsPage(Page);

        await diagnosticsPage
            .NavigateToDiagnosticsAsync();

        await DismissConsentPopupIfPresentAsync();

        TestContext.Out.WriteLine(
            "Running axe accessibility scan...");

        var results =
            await Page.RunAxe();

        Assert.That(
            results,
            Is.Not.Null,
            "Expected axe to return accessibility scan results.");

        TestContext.Out.WriteLine(
            $"Accessibility violations found: " +
            $"{results.Violations.Length}");

        TestContext.Out.WriteLine(
            "========================================");

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
                "----------------------------------------");
        }

        // Report all violations, but fail the quality
        // gate for serious or critical findings.
        var seriousOrCriticalViolations =
            results.Violations
                .Where(violation =>
                {
                    var impact =
                        violation.Impact?.ToString();

                    return string.Equals(
                               impact,
                               "serious",
                               StringComparison.OrdinalIgnoreCase)
                           ||
                           string.Equals(
                               impact,
                               "critical",
                               StringComparison.OrdinalIgnoreCase);
                })
                .ToList();

        TestContext.Out.WriteLine(
            $"Serious/Critical violations: " +
            $"{seriousOrCriticalViolations.Count}");

        TestContext.Out.WriteLine(
            "Accessibility quality gate: " +
            "Serious/Critical <= 0");

        Assert.That(
            seriousOrCriticalViolations,
            Is.Empty,
            "Accessibility quality gate failed: " +
            "serious or critical violations were found.");

        TestContext.Out.WriteLine(
            "Accessibility quality gate passed.");

        TestContext.Out.WriteLine(
            "Accessibility scan completed successfully.");
    }

    [Test]
    public async Task CorporateWellnessFormHasLogicalKeyboardFocusOrder()
    {
        var corporateWellnessPage =
            new CorporateWellnessPage(Page);

        await corporateWellnessPage
            .NavigateAsync();

        await DismissConsentPopupIfPresentAsync();

        TestContext.Out.WriteLine(
            "Starting keyboard focus-order accessibility test...");

        // Make keyboard focus clearly visible when
        // running the test in headed mode.
        await Page.AddStyleTagAsync(
            new()
            {
                Content =
                    """
                    *:focus {
                        outline: 4px solid red !important;
                        outline-offset: 4px !important;
                    }
                    """
            });

        // Expected logical order of the main form controls.
        var expectedFocusOrder =
            new[]
            {
                "name",
                "organizationName",
                "contactNumber",
                "officialEmailId",
                "organizationSize",
                "interestedIn"
            };

        var actualFocusOrder =
            new List<string>();

        const int maximumTabPresses = 20;

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
                            element.getAttribute("type") ?? "",
                            element.getAttribute("placeholder") ?? "",
                            element.id ?? "",
                            element.textContent?.trim() ?? ""
                        ]
                        .filter(Boolean)
                        .join(" | ");
                    }
                    """);

            TestContext.Out.WriteLine(
                $"Tab {tabNumber}: {focusedElement}");

            var focusedElementId =
                await Page.EvaluateAsync<string>(
                    """
                    () => {
                        const element =
                            document.activeElement;

                        return element?.id ?? "";
                    }
                    """);

            if (expectedFocusOrder.Contains(focusedElementId) &&
                !actualFocusOrder.Contains(focusedElementId))
            {
                actualFocusOrder.Add(
                    focusedElementId);

                TestContext.Out.WriteLine(
                    $"Form control reached: " +
                    $"{focusedElementId}");
            }

            if (actualFocusOrder.Count ==
                expectedFocusOrder.Length)
            {
                break;
            }
        }

        TestContext.Out.WriteLine(
            "========================================");

        TestContext.Out.WriteLine(
            "Expected focus order:");

        foreach (var control in expectedFocusOrder)
        {
            TestContext.Out.WriteLine(
                $"  {control}");
        }

        TestContext.Out.WriteLine(
            "Actual focus order:");

        foreach (var control in actualFocusOrder)
        {
            TestContext.Out.WriteLine(
                $"  {control}");
        }

        Assert.That(
            actualFocusOrder,
            Is.EqualTo(expectedFocusOrder),
            "The Corporate Wellness form controls " +
            "were not reached in the expected " +
            "keyboard focus order.");

        TestContext.Out.WriteLine(
            "Keyboard focus order verified successfully.");
    }
}