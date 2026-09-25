using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Practo.HospitalFinder.Security.Tests;

[TestFixture]
public class SeleniumZapTests
{
    [Test]
    public void ExercisePractoThroughZap()
    {
        var proxy = new Proxy
        {
            HttpProxy = SecurityConfig.ZapProxy,
            SslProxy = SecurityConfig.ZapProxy
        };

        var options = new ChromeOptions
        {
            Proxy = proxy,
            AcceptInsecureCertificates = true
        };

        using var driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl(SecurityConfig.Target);

        var title = driver.Title;

        var bodyText = driver
            .FindElement(By.TagName("body"))
            .Text;

        Assert.That(
            title,
            Is.Not.Empty,
            "Expected Practo to have a page title.");

        Assert.That(
            bodyText,
            Is.Not.Empty,
            "Expected Practo to contain visible content.");

        TestContext.Out.WriteLine(
            $"Visited through ZAP: {SecurityConfig.Target}");

        TestContext.Out.WriteLine(
            $"Page title: {title}");
    }
}