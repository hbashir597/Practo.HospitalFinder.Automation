# Test Plan - Practo Hospital Finder

## 1. Objective

The purpose of this capstone is to demonstrate the testing skills developed during the QEA SDET C# technical training using the Practo Hospital Finder project.

The solution combines functional and non-functional testing techniques within a structured automation framework.

## 2. Scope

The three main functional journeys are:

1. **Hospital Search**
   - Search for hospitals in Bangalore.
   - Identify hospitals open 24/7.
   - Validate that the rating is greater than 3.5.
   - Check hospital details for parking availability.
   - Collect and display qualifying hospital names.

2. **Diagnostics**
   - Navigate to Diagnostics.
   - Extract the displayed top cities.
   - Store the city names in a collection.
   - Display and validate the collected city information.

3. **Corporate Wellness**
   - Navigate to Corporate Wellness.
   - Enter invalid details.
   - Attempt to submit the form.
   - Capture and validate the resulting warning or validation behaviour.

## 3. Test Approach

The project demonstrates:

- **Playwright + C# + NUnit** for functional UI automation.
- **Selenium WebDriver + Reqnroll** for BDD automation.
- **Page Object Model (POM)** for framework design.
- **Parallel execution** where appropriate.
- **Allure and Extent Reports** for reporting and execution evidence.
- **Serilog** for automated test logging.
- **Apache JMeter** for performance testing.
- **Playwright + axe-core** for automated accessibility scanning.
- **Keyboard accessibility testing** for logical focus order.
- **OWASP ZAP** for passive security testing.
- **Docker and Selenium Grid** for remote/containerised browser execution.

Test design includes positive and negative scenarios where relevant.

## 4. Test Evidence

Evidence produced by the project includes:

- Automated test results.
- Allure and Extent reports.
- Failure screenshots and logs where appropriate.
- JMeter performance results.
- Accessibility scan results.
- Keyboard accessibility results.
- OWASP ZAP passive scan results and reports.
- Recorded demonstrations of selected test executions.
- Requirements Traceability Matrix (RTM).

## 5. Quality Gates

Quality gates are used where appropriate to convert non-functional findings into automated pass/fail criteria.

Examples include:

- Accessibility testing reports all axe violations while failing the automated quality gate if serious or critical violations are detected.
- Security testing evaluates passive OWASP ZAP findings against defined severity thresholds.

The purpose of these gates is to distinguish between reporting findings and determining whether the automated test should fail.

## 6. Constraints

Practo is a live third-party website and may change independently of the automation project.

Available controls, filters, content and validation behaviour were therefore verified against the live website before automation.

The Hospital Finder requirement cannot rely entirely on search-page filters because all required criteria are not directly available as filters. The automation therefore identifies candidate hospitals from the listing and validates additional information from hospital details where required.

The website may also return consent dialogs or challenge-validation behaviour, so the automation includes defensive handling and explicit waits where appropriate.

Only passive security testing is performed against the public Practo website.

Performance testing is limited to controlled demonstration-level execution rather than high-volume load generation against the public website.

## 7. Out of Scope

- API testing.
- CI/CD pipeline implementation.
- Database testing.
- Active or intrusive security testing.
- High-volume load or stress testing against the public Practo website.