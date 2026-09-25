using Practo.HospitalFinder.Security;

namespace Practo.HospitalFinder.Security.Tests;

[TestFixture]
public class ZapConnectionTests
{
    [Test]
    public async Task ZapApiIsReachable()
    {
        var zap = new ZapApiClient();

        var version = await zap.VersionAsync();

        TestContext.Out.WriteLine(
            $"Connected to OWASP ZAP version: {version}");

        Assert.That(
            version,
            Is.Not.EqualTo("unknown"),
            "Expected the ZAP API to return its version.");
    }
}