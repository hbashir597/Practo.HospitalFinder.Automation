using System.Text.Json;

namespace Practo.HospitalFinder.Security;

public sealed class ZapApiClient
{
    private readonly HttpClient _httpClient = new();

    private static string BuildUrl(
        string path,
        IDictionary<string, string?>? query = null)
    {
        var items = new List<string>();

        if (!string.IsNullOrWhiteSpace(SecurityConfig.ApiKey))
        {
            items.Add(
                "apikey=" +
                Uri.EscapeDataString(SecurityConfig.ApiKey));
        }

        if (query != null)
        {
            items.AddRange(
                query
                    .Where(item => item.Value != null)
                    .Select(item =>
                        Uri.EscapeDataString(item.Key) +
                        "=" +
                        Uri.EscapeDataString(item.Value!)));
        }

        return SecurityConfig.ZapBaseUrl +
               path +
               (items.Count == 0
                   ? ""
                   : "?" + string.Join("&", items));
    }

    private async Task<JsonDocument> GetJsonAsync(
        string path,
        IDictionary<string, string?>? query = null)
    {
        var json = await _httpClient.GetStringAsync(
            BuildUrl(path, query));

        return JsonDocument.Parse(json);
    }

    public async Task<string> VersionAsync()
    {
        using var json = await GetJsonAsync(
            "/JSON/core/view/version/");

        return json.RootElement
                   .GetProperty("version")
                   .GetString()
               ?? "unknown";
    }

    public async Task WaitForPassiveScanAsync()
    {
        while (true)
        {
            using var json = await GetJsonAsync(
                "/JSON/pscan/view/recordsToScan/");

            var remaining = int.Parse(
                json.RootElement
                    .GetProperty("recordsToScan")
                    .GetString()!);

            TestContext.Out.WriteLine(
                $"Passive records remaining: {remaining}");

            if (remaining == 0)
            {
                break;
            }

            await Task.Delay(2000);
        }
    }

    public async Task<string> AlertsJsonAsync()
    {
        return await _httpClient.GetStringAsync(
            BuildUrl(
                "/JSON/core/view/alerts/",
                new Dictionary<string, string?>
                {
                    ["baseurl"] = SecurityConfig.Target
                }));
    }

    public async Task<string> SaveAlertsReportAsync()
    {
        var alertsJson = await AlertsJsonAsync();

        var reportDirectory = Path.GetFullPath(
            Path.Combine(
                AppContext.BaseDirectory,
                "..",
                "..",
                "..",
                "Reports"));

        Directory.CreateDirectory(reportDirectory);

        var reportPath = Path.Combine(
            reportDirectory,
            "zap-practo-alerts.json");

        await File.WriteAllTextAsync(
            reportPath,
            alertsJson);

        TestContext.Out.WriteLine(
            $"ZAP alerts report saved to: {reportPath}");

        return reportPath;
    }
}