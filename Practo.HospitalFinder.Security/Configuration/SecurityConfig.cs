namespace Practo.HospitalFinder.Security;

public static class SecurityConfig
{
    public const string Target = "https://www.practo.com/";

    public static string ZapHost =>
        Environment.GetEnvironmentVariable("ZAP_HOST") ?? "localhost";

    public static int ZapPort =>
        int.TryParse(
            Environment.GetEnvironmentVariable("ZAP_PORT"),
            out var port)
            ? port
            : 8080;

    public static string ApiKey =>
        Environment.GetEnvironmentVariable("ZAP_API_KEY") ?? "";

    public static string ZapProxy =>
        $"{ZapHost}:{ZapPort}";

    public static string ZapBaseUrl =>
        $"http://{ZapHost}:{ZapPort}";

    public const int MaxHighAlerts = 0;
    public const int MaxMediumAlerts = 5;
}