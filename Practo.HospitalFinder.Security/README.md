# Practo Hospital Finder - Security Testing

## Overview

This project demonstrates basic security testing of the Practo website using
OWASP ZAP, Selenium WebDriver, C# and NUnit.

The testing is intentionally limited to passive security analysis because
Practo is a public third-party website.

No active scanning, spidering, forced browsing, fuzzing or exploitation is
performed.

## Tools

- C#
- .NET 10
- NUnit
- Selenium WebDriver
- OWASP ZAP 2.17.0

## Approach

Selenium launches Chrome and routes normal browser traffic through the local
OWASP ZAP proxy.

The flow is:

C# Test
→ Selenium WebDriver
→ OWASP ZAP Proxy
→ Practo
→ Passive Scan
→ ZAP Alerts
→ NUnit Results / JSON Evidence

ZAP runs locally on:

`localhost:8080`

The target used for this exercise is:

`https://www.practo.com/`

## Tests

### ZAP API Connection

`ZapApiIsReachable`

Verifies that the C# automation can communicate with the locally running
OWASP ZAP API.

### Selenium Through ZAP

`ExercisePractoThroughZap`

Launches Chrome using ZAP as the HTTP/HTTPS proxy and performs normal,
low-volume navigation to Practo.

This generates traffic for ZAP to analyse passively.

### Passive Security Audit

`PractoPassiveScanCollectsAlerts`

Waits for ZAP's passive scanning queue to complete, retrieves alerts scoped
to Practo, displays the findings and saves the raw results as JSON evidence.

### Security Quality Gate

`PractoPassiveScanMeetsQualityGate`

Evaluates the passive findings against the configured thresholds:

- High alerts: maximum 0
- Medium alerts: maximum 5

The quality gate is intentionally kept separate from the passive audit so
that successful security scanning is distinguished from whether the target
meets the chosen thresholds.

## Observed Results

During the recorded run, ZAP reported:

- High: 0
- Medium: 9
- Low: 20
- Informational: 4
- Total: 33

The passive audit completed successfully.

The quality gate failed because 9 Medium alerts were observed while the
configured maximum was 5.

The threshold was not increased simply to make the test pass.

These results represent passive observations from the recorded run and
should not be interpreted as confirmation of exploitable vulnerabilities.

## Example Findings

Medium-risk findings included:

- CSP: Failure to Define Directive with No Fallback
- CSP: Wildcard Directive
- CSP: script-src unsafe-inline
- CSP: style-src unsafe-inline
- Sub Resource Integrity Attribute Missing

Lower-risk and informational findings included cookie configuration,
timestamp disclosure, cross-domain JavaScript inclusion and security-header
observations.

## Evidence

Raw Practo-scoped ZAP alert data is saved to:

`Reports/zap-practo-alerts.json`

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

The project only analyses traffic generated through normal browser
interaction.