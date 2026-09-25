using Practo.HospitalFinder.Security;
using System.Text.Json;

namespace Practo.HospitalFinder.Security.Tests;

[TestFixture]
public class PassiveSecurityTests
{
    private static async Task<List<JsonElement>> GetPractoAlertsAsync()
    {
        var zap = new ZapApiClient();

        var version = await zap.VersionAsync();

        TestContext.Out.WriteLine(
            $"Connected to OWASP ZAP version: {version}");

        await zap.WaitForPassiveScanAsync();

        var alertsJson = await zap.AlertsJsonAsync();

        using var document = JsonDocument.Parse(alertsJson);

        return document.RootElement
            .GetProperty("alerts")
            .EnumerateArray()
            .Select(alert => alert.Clone())
            .ToList();
    }

    private static void PrintAlertSummary(
        List<JsonElement> alerts)
    {
        var highAlerts =
            CountAlertsByRisk(alerts, "High");

        var mediumAlerts =
            CountAlertsByRisk(alerts, "Medium");

        var lowAlerts =
            CountAlertsByRisk(alerts, "Low");

        var informationalAlerts =
            CountAlertsByRisk(alerts, "Informational");

        TestContext.Out.WriteLine(
            $"Total Practo alerts: {alerts.Count}");

        TestContext.Out.WriteLine(
            $"High: {highAlerts}");

        TestContext.Out.WriteLine(
            $"Medium: {mediumAlerts}");

        TestContext.Out.WriteLine(
            $"Low: {lowAlerts}");

        TestContext.Out.WriteLine(
            $"Informational: {informationalAlerts}");
    }

    private static int CountAlertsByRisk(
        List<JsonElement> alerts,
        string risk)
    {
        return alerts.Count(alert =>
            alert.GetProperty("risk").GetString() == risk);
    }

    [Test]
    public async Task PractoPassiveScanCollectsAlerts()
    {
        var alerts = await GetPractoAlertsAsync();

        PrintAlertSummary(alerts);

        foreach (var alert in alerts)
        {
            var name = alert
                .GetProperty("alert")
                .GetString();

            var risk = alert
                .GetProperty("risk")
                .GetString();

            var url = alert
                .GetProperty("url")
                .GetString();

            TestContext.Out.WriteLine(
                $"[{risk}] {name}");

            TestContext.Out.WriteLine(
                $"URL: {url}");
        }

        var zap = new ZapApiClient();

        var reportPath =
            await zap.SaveAlertsReportAsync();

        Assert.Multiple(() =>
        {
            Assert.That(
                alerts,
                Is.Not.Null,
                "Expected ZAP to return passive scan results.");

            Assert.That(
                File.Exists(reportPath),
                Is.True,
                "Expected the ZAP alerts report to be created.");
        });
    }

    [Test]
    public async Task PractoPassiveScanMeetsQualityGate()
    {
        var alerts = await GetPractoAlertsAsync();

        PrintAlertSummary(alerts);

        var highAlerts =
            CountAlertsByRisk(alerts, "High");

        var mediumAlerts =
            CountAlertsByRisk(alerts, "Medium");

        TestContext.Out.WriteLine(
            $"Quality gate: High <= {SecurityConfig.MaxHighAlerts}");

        TestContext.Out.WriteLine(
            $"Quality gate: Medium <= {SecurityConfig.MaxMediumAlerts}");

        Assert.Multiple(() =>
        {
            Assert.That(
                highAlerts,
                Is.LessThanOrEqualTo(
                    SecurityConfig.MaxHighAlerts),
                $"High-risk alerts exceeded the threshold of {SecurityConfig.MaxHighAlerts}.");

            Assert.That(
                mediumAlerts,
                Is.LessThanOrEqualTo(
                    SecurityConfig.MaxMediumAlerts),
                $"Medium-risk alerts exceeded the threshold of {SecurityConfig.MaxMediumAlerts}.");
        });
    }
}