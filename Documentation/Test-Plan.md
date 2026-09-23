# Test Plan - Practo Hospital Finder

## 1. Objective

The purpose of this capstone is to demonstrate the testing skills developed during the QEA SDET C# technical training using the Practo Hospital Finder project.

## 2. Scope

The three main functional journeys are:

1. **Hospital Search**
   - Search for hospitals in Bangalore.
   - Identify hospitals open 24/7.
   - Check for parking availability.
   - Validate rating is greater than 3.5.
   - Collect and display qualifying hospital names.

2. **Diagnostics**
   - Navigate to Diagnostics.
   - Extract the displayed top cities.
   - Store and display the city names using a collection.

3. **Corporate Wellness**
   - Navigate to Corporate Wellness.
   - Enter invalid details.
   - Attempt to submit/schedule.
   - Capture and validate the warning or validation message.

## 3. Test Approach

The project will demonstrate:

- **Playwright + C# + NUnit** for functional UI automation.
- **Selenium WebDriver + Reqnroll** for BDD automation.
- **Page Object Model (POM)** for framework design.
- **Parallel execution** where appropriate.
- **Allure and Extent Reports** for reporting.
- **JMeter** for performance testing.
- **Accessibility testing** including automated and keyboard checks.
- **OWASP ZAP** for passive security testing.
- **Docker and Selenium Grid** for remote/containerised browser execution.

Test design will include positive and negative scenarios where relevant.

## 4. Test Evidence

Evidence will include:

- Automated test results
- Allure and Extent reports
- Screenshots and logs where appropriate
- JMeter performance results
- Accessibility results
- OWASP ZAP passive scan results
- Requirement Traceability Matrix (RTM)

## 5. Constraints

Practo is a live third-party website and may change during development.

Available controls, filters and validation behaviour will therefore be verified against the current website before automation.

Any difference between the supplied project brief and the live website will be documented rather than forcing an inaccurate test.

Only passive security testing will be performed against the public Practo website.

## 6. Out of Scope

- API testing
- CI/CD
- Database testing
- Active or intrusive security testing