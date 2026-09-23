# Requirements Traceability Matrix - Practo Hospital Finder

| Requirement | Description | Test Case | Automation |
|---|---|---|---|
| REQ-01 | Find qualifying hospitals in Bangalore: open 24/7, parking available and rating > 3.5 | TC-01 | Playwright |
| REQ-02 | Extract and display the top cities from Diagnostics | TC-02 | Playwright |
| REQ-03 | Submit invalid Corporate Wellness details and capture the warning/validation message | TC-03 | Selenium + Reqnroll BDD |

## Additional Coverage

| Area | Tool |
|---|---|
| Performance Testing | JMeter |
| Accessibility Testing | Playwright / accessibility tooling |
| Security Testing | OWASP ZAP |
| Reporting | Allure + Extent Reports |
| Containerised Execution | Docker + Selenium Grid |