using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Practo.HospitalFinder.SeleniumBDD.Reporting;

public static class ExtentReport
{
    public static readonly ExtentReports Instance;

    static ExtentReport()
    {
        var projectDirectory =
            Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    ".."));

        var reportDirectory =
            Path.Combine(
                projectDirectory,
                "TestResults");

        Directory.CreateDirectory(reportDirectory);

        var reportPath =
            Path.Combine(
                reportDirectory,
                "ExtentReport.html");

        var sparkReporter =
            new ExtentSparkReporter(reportPath);

        sparkReporter.Config.DocumentTitle =
            "Practo Hospital Finder Automation";

        Instance = new ExtentReports();

        Instance.AttachReporter(sparkReporter);

        Instance.AddSystemInfo(
            "Framework",
            "Reqnroll + NUnit + Selenium");

        Instance.AddSystemInfo(
            "Application",
            "Practo Hospital Finder");
    }
}