# Test Cases - Practo Hospital Finder

## Test Case Summary

| ID | Scenario | Type | Tool / Framework |
|---|---|---|---|
| TC-01 | Find qualifying hospitals in Bangalore | Functional / Positive | Playwright |
| TC-02 | Extract top Diagnostics cities | Functional / Positive | Playwright |
| TC-03 | Validate Corporate Wellness invalid submission | Functional / Negative | Playwright |
| TC-04 | Validate hospital search using BDD | Functional / BDD | Selenium + Reqnroll |
| TC-05 | Automated accessibility scan | Accessibility | Playwright + axe-core |
| TC-06 | Validate Corporate Wellness keyboard focus order | Accessibility | Playwright |
| TC-07 | Corporate Wellness performance test | Performance | Apache JMeter |
| TC-08 | Passive security assessment | Security | OWASP ZAP |
| TC-09 | Execute Selenium tests remotely | Infrastructure | Docker + Selenium Grid |

---

## TC-01 - Find Qualifying Hospitals in Bangalore

**Test Data**

- Location: Bangalore
- Search type: Hospitals
- Required availability: Open 24/7
- Minimum rating: Greater than 3.5
- Required amenity: Parking

**Steps**

1. Navigate to the Bangalore hospital listing.
2. Extract hospital information from the displayed results.
3. Identify candidate hospitals that are open 24/7.
4. Validate that the hospital rating is greater than 3.5.
5. Open the hospital details where required.
6. Check whether parking is listed as an amenity.
7. Display the qualifying hospital information.

**Expected Result**

Hospitals satisfying the required criteria are successfully identified and the matching information is displayed.

---

## TC-02 - Extract Top Diagnostics Cities

**Steps**

1. Navigate to the Diagnostics section.
2. Locate the displayed top cities.
3. Extract the city names.
4. Store the names in a collection.
5. Display and validate the collected city information.

**Expected Result**

The displayed top city names are successfully collected and validated.

---

## TC-03 - Corporate Wellness Invalid Submission

**Test Data**

Invalid form details are defined in `Test-Data.md`.

**Steps**

1. Navigate to Corporate Wellness.
2. Enter invalid details into the required fields.
3. Attempt to submit the form.
4. Capture the resulting warning or validation behaviour.
5. Validate that the invalid submission is not accepted as a successful request.

**Expected Result**

The invalid submission is prevented and the expected validation behaviour is observed.

---

## TC-04 - Selenium BDD Hospital Search

**Steps**

1. Execute the hospital search scenario defined in the Reqnroll feature file.
2. Navigate to the required Practo hospital listing using Selenium WebDriver.
3. Perform the scenario steps defined using Gherkin.
4. Validate the expected outcome using the associated step definitions.

**Expected Result**

The BDD scenario executes successfully and the expected behaviour is validated.

---

## TC-05 - Automated Accessibility Scan

**Steps**

1. Navigate to the Diagnostics page.
2. Run an axe-core accessibility scan.
3. Collect all reported accessibility violations.
4. Display each violation with its impact and description.
5. Count serious and critical violations.
6. Apply the accessibility quality gate.

**Expected Result**

Accessibility findings are reported successfully.

The automated quality gate passes only when no serious or critical accessibility violations are detected.

---

## TC-06 - Corporate Wellness Keyboard Focus Order

**Steps**

1. Navigate to the Corporate Wellness form.
2. Use keyboard Tab navigation.
3. Record each relevant form control as it receives focus.
4. Compare the actual focus sequence with the expected logical sequence.

**Expected Result**

The main Corporate Wellness form controls are reachable by keyboard in the expected logical order.

---

## TC-07 - Corporate Wellness Performance Test

**Steps**

1. Execute the configured Apache JMeter test plan.
2. Send the controlled test requests defined by the performance scenario.
3. Capture response-time, throughput and error information.
4. Review the generated performance results.

**Expected Result**

The performance test completes and produces measurable performance results without using intrusive or high-volume load against the public website.

---

## TC-08 - Passive Security Assessment

**Steps**

1. Start OWASP ZAP.
2. Route the automated browser journey through the ZAP proxy.
3. Allow ZAP passive scanning to analyse captured traffic.
4. Retrieve and classify the generated alerts.
5. Apply the configured security quality criteria.
6. Produce security evidence and reports.

**Expected Result**

Passive security findings are successfully captured, classified and reported without performing an active attack against the public website.

---

## TC-09 - Docker Selenium Grid Execution

**Steps**

1. Start the Selenium Grid environment using Docker Compose.
2. Connect the Selenium framework using `RemoteWebDriver`.
3. Execute the selected Selenium BDD tests.
4. Observe remote browser execution using noVNC where required.
5. Verify the test result.

**Expected Result**

The Selenium automation executes successfully against the containerised remote Chrome browser.

---

## Notes and Constraints

Practo is a live third-party website, so its content and behaviour can change independently of this project.

During implementation, the live Hospital Finder journey was inspected before automation. Not all required criteria were available as direct search filters, so the automation identifies suitable candidates from the hospital listing and validates additional information from hospital details where required.

Consent dialogs and challenge-validation behaviour may also appear during automated execution. Defensive handling and explicit waits are used where appropriate.

Security testing is passive only, and performance testing is limited to controlled demonstration-level execution against the public website.