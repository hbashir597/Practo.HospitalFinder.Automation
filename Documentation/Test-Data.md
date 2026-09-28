# Test Data - Practo Hospital Finder

## Functional Test Data

| Area | Data | Purpose |
|---|---|---|
| Hospital Search | Bangalore | Hospital search location |
| Hospital Search | Hospitals | Search/listing type |
| Hospital Search | Open 24/7 | Required availability criterion |
| Hospital Search | Rating > 3.5 | Required rating threshold |
| Hospital Details | Parking | Required hospital amenity |
| Diagnostics | Top Cities | Collection/extraction validation |
| Corporate Wellness | Invalid form data | Negative form validation |

## Corporate Wellness Test Data

The Corporate Wellness scenario uses controlled invalid or test data to exercise the form validation behaviour.

The form fields covered include:

- Name
- Organization Name
- Contact Number
- Official Email ID
- Organization Size
- Interested In

No genuine personal information is required for the automated test.

## Accessibility Test Data

The accessibility tests use:

- Diagnostics page for the automated axe-core scan.
- Corporate Wellness form for keyboard focus-order validation.
- Serious/Critical violations: `0` permitted by the automated accessibility quality gate.

Lower-severity accessibility findings are reported without automatically failing the test.

## Performance Test Data

Apache JMeter uses the configured Corporate Wellness performance journey.

Performance evidence includes:

- Response time.
- Throughput.
- Error rate.

Execution is intentionally controlled because Practo is a live third-party website.

## Security Test Data

OWASP ZAP testing uses traffic generated against the public Practo website.

The security test uses:

- Passive scanning only.
- Captured HTTP/HTTPS traffic.
- ZAP alert risk/severity information.
- Defined quality-gate criteria within the automated security tests.

No active or intrusive security attacks are performed.

## Notes

Practo is a live third-party website, so displayed hospitals, ratings, availability, amenities and other site content may change over time.

The automation therefore validates the current live data rather than relying on permanently hard-coded hospital results.

Test data is limited to non-sensitive demonstration data. No real personal information is required for form testing.