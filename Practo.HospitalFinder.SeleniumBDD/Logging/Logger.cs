using Serilog;

namespace Practo.HospitalFinder.SeleniumBDD.Logging;

public static class Logger
{
    private static bool _initialised;

    public static void Initialise()
    {
        if (_initialised)
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

        var logDirectory =
            Path.Combine(
                projectDirectory,
                "TestResults",
                "Logs");

        Directory.CreateDirectory(
            logDirectory);

        var logPath =
            Path.Combine(
                logDirectory,
                "test-log-.txt");

        Log.Logger =
            new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    logPath,
                    rollingInterval:
                        RollingInterval.Day,
                    outputTemplate:
                        "[{Timestamp:yyyy-MM-dd HH:mm:ss}] " +
                        "[{Level:u3}] " +
                        "{Message:lj}" +
                        "{NewLine}{Exception}")
                .CreateLogger();

        _initialised = true;

        Log.Information(
            "BDD test logging initialised");
    }

    public static void Info(
        string message)
    {
        Log.Information(message);
    }

    public static void Error(
        string message,
        Exception? exception = null)
    {
        if (exception is null)
        {
            Log.Error(message);
        }
        else
        {
            Log.Error(
                exception,
                message);
        }
    }

    public static void Close()
    {
        Log.Information(
            "BDD test logging completed");

        Log.CloseAndFlush();

        _initialised = false;
    }
}