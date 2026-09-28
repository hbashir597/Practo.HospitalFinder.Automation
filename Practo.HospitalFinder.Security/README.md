# Practo Hospital Finder - Security Testing

## Overview

This project demonstrates basic security testing of the Practo website using OWASP ZAP, Selenium WebDriver, C# and NUnit.

The testing is intentionally limited to passive security analysis because Practo is a public third-party website.

No active scanning, spidering, forced browsing, fuzzing or exploitation is performed.

## Tools

- C#
- .NET 10
- NUnit
- Selenium WebDriver
- OWASP ZAP 2.17.0

## Security Testing Approach

Selenium launches Chrome and routes normal browser traffic through the local OWASP ZAP proxy.

The execution flow is:

```text
C# / NUnit Test
        |
        v
Selenium WebDriver
        |
        v
OWASP ZAP Proxy
        |
        v
Practo
        |
        v
Passive Scan
        |
        v
ZAP Alerts
        |
        v
NUnit Results / JSON / HTML Evidence
```

ZAP runs locally on:

```text
http://localhost:8080
```

The target used for the security testing is:

```text
https://www.practo.com/
```

## Automated Tests

### ZAP API Connection

`ZapApiIsReachable`

Verifies that the C# automation framework can communicate successfully with the locally running OWASP ZAP API.

### Selenium Through ZAP

`ExercisePractoThroughZap`

Launches Chrome using ZAP as the HTTP and HTTPS proxy and performs normal, low-volume navigation to Practo.

This generates legitimate browser traffic for ZAP to analyse passively.

### Passive Security Audit

`PractoPassiveScanCollectsAlerts`

Waits for ZAP's passive scanning queue to complete, retrieves alerts scoped to Practo, displays the findings and saves the raw results as JSON evidence.

### Security Quality Gate

`PractoPassiveScanMeetsQualityGate`

Evaluates the passive findings against the configured thresholds:

- High alerts: maximum 0
- Medium alerts: maximum 5

The quality gate is intentionally kept separate from the passive audit.

This distinguishes successful execution of the security scan from whether the application actually satisfies the chosen security thresholds.

A failed quality gate therefore represents a valid test result rather than a failure of the automation framework.

## Observed Results

During the recorded security test run, ZAP reported:

- High: 0
- Medium: 9
- Low: 20
- Informational: 4
- Total: 33

The passive security audit completed successfully.

The security quality gate failed because 9 Medium alerts were observed while the configured maximum was 5.

The threshold was deliberately not increased simply to make the test pass.

This demonstrates the use of an objective quality gate where the test result reflects the observed security findings.

Because Practo is a live third-party website, the number and type of alerts may change between executions.

These findings represent passive observations from the recorded test run and should not be interpreted as confirmation of exploitable vulnerabilities.

## Example Findings

Medium-risk findings included:

- CSP: Failure to Define Directive with No Fallback
- CSP: Wildcard Directive
- CSP: script-src unsafe-inline
- CSP: style-src unsafe-inline
- Sub Resource Integrity Attribute Missing

Lower-risk and informational findings included observations relating to:

- Cookie configuration
- Timestamp disclosure
- Cross-domain JavaScript inclusion
- Security headers
- Session management information

## Evidence and Reporting

Raw Practo-scoped ZAP alert data is saved to:

```text
Reports/zap-practo-alerts.json
```

ZAP HTML reports are also stored in the `Reports` directory to provide human-readable evidence of the passive security findings.

The security evidence can therefore be reviewed through:

- NUnit test output.
- Console output.
- Raw JSON alert data.
- ZAP HTML reporting.

## Safety Boundary

Practo is a public third-party website.

For this reason, this project uses passive scanning only.

The following ZAP capabilities are not used against Practo:

- Active Scan
- Spider
- Client Spider
- Forced Browse
- Fuzzer
- Exploit verification

The project only analyses traffic generated through normal browser interaction.

This keeps the security testing non-intrusive while still demonstrating automated integration between Selenium, C#, NUnit and OWASP ZAP.

## Current Status

The security testing framework is complete and operational.

The project successfully demonstrates:

- Communication with the ZAP API.
- Selenium browser traffic routed through ZAP.
- Passive vulnerability scanning.
- Retrieval and classification of ZAP alerts.
- JSON evidence generation.
- HTML security reporting.
- An automated security quality gate.
- Safe testing boundaries for a public third-party website.

The recorded execution produced valid passive security findings and demonstrated that the configured quality gate correctly fails when the Medium-risk alert threshold is exceeded.