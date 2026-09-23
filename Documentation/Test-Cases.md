# Test Cases - Practo Hospital Finder

## Test Case Summary

| ID | Scenario | Type | Automation |
|---|---|---|---|
| TC-01 | Find qualifying hospitals in Bangalore | Positive | Playwright |
| TC-02 | Extract top Diagnostics cities | Positive | Playwright |
| TC-03 | Validate Corporate Wellness invalid submission | Negative | Selenium BDD |

---

## TC-01 - Find Qualifying Hospitals in Bangalore

**Test Data**
- Location: Bangalore
- Search: Hospital
- Rating: Greater than 3.5

**Steps**
1. Open Practo.
2. Search for hospitals in Bangalore.
3. Identify hospitals that are open 24/7.
4. Check for parking availability.
5. Validate that the rating is greater than 3.5.
6. Store and display the names of qualifying hospitals.

**Expected Result**

Hospital names satisfying the available required criteria are successfully identified and displayed.

---

## TC-02 - Extract Top Diagnostics Cities

**Steps**
1. Navigate to the Diagnostics section.
2. Locate the displayed top cities.
3. Extract the city names.
4. Store the names in a collection.
5. Display the collected city names.

**Expected Result**

The displayed top city names are successfully collected and displayed.

---

## TC-03 - Corporate Wellness Invalid Submission

**Test Data**
- Invalid form details defined in `Test-Data.md`.

**Steps**
1. Navigate to Corporate Wellness.
2. Enter invalid details.
3. Attempt to submit or schedule the request.
4. Capture the warning, alert or validation message.

**Expected Result**

The invalid submission is prevented and the relevant warning or validation message is captured successfully.

---

## Note

Practo is a live website. Exact filters, controls and validation behaviour will be confirmed against the current site during implementation.

If the live website differs from the supplied project brief, the difference will be documented.