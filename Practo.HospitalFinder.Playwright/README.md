# Practo Hospital Finder - Playwright

## Purpose

This project contains the Playwright-based functional and accessibility automation for the Practo Hospital Finder capstone project.

Playwright is used for the primary functional user journeys and accessibility testing against the Practo website.

## Technology

- C#
- .NET
- NUnit
- Microsoft Playwright
- axe-core
- Allure
- Page Object Model

## Functional Coverage

The Playwright test suite covers the main functional journeys from the project specification.

### Hospital Finder

The Hospital Finder automation:

- Navigates to the Bangalore hospital listing.
- Extracts information from multiple hospital results.
- Identifies hospitals that are open 24/7.
- Validates that ratings are greater than 3.5.
- Opens hospital details where required.
- Checks for parking availability.
- Displays qualifying hospital information.

Because all required criteria are not available as direct filters on the live Practo website, the framework identifies candidate hospitals from the search results and validates additional information from the hospital details page.

### Diagnostics

The Diagnostics automation:

- Navigates to the Diagnostics page.
- Extracts the displayed top city information.
- Stores extracted information in a collection.
- Validates the collected results.

### Corporate Wellness

The Corporate Wellness automation:

- Navigates to the Corporate Wellness page.
- Interacts with the form.
- Uses invalid test data for negative testing.
- Validates the resulting form behaviour.

## Accessibility Testing

The project also contains Playwright-based accessibility tests.

### Automated axe Scan

The automated accessibility test:

- Navigates to the Diagnostics page.
- Runs an axe-core accessibility scan.
- Reports all detected violations.
- Displays the rule, impact, description and help information.
- Counts serious and critical violations.
- Applies an automated accessibility quality gate.

The quality gate permits zero serious or critical violations.

Lower-severity findings are still reported so that they remain visible without automatically failing the test.

### Keyboard Focus Order

A separate accessibility test validates keyboard navigation through the Corporate Wellness form.

The test:

- Uses Tab-based keyboard navigation.
- Records the form controls as they receive focus.
- Compares the actual focus sequence against the expected logical order.
- Fails if the expected focus order is not followed.

The keyboard test can be executed in headed mode to make the focus movement visible during demonstration.

## Framework Design

The Playwright framework uses:

- Page Object Model.
- Reusable page objects.
- Playwright locators.
- Explicit waiting for dynamic content.
- NUnit assertions.
- Parallel execution where appropriate.
- Defensive handling for live-site behaviour.
- Failure screenshots.
- Allure reporting.

The framework also detects unexpected Practo challenge-validation pages where relevant so that these are not mistaken for valid application results.

## Page Objects

The project contains page objects for the main application areas, including:

- `HospitalSearchPage`
- `HospitalDetailsPage`
- `DiagnosticsPage`
- `CorporateWellnessPage`

This keeps page interaction logic separate from the test classes.

## Running the Tests

From the repository root, the Playwright project can be executed with:

```bash
dotnet test Practo.HospitalFinder.Playwright