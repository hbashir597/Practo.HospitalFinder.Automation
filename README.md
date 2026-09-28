# Practo Hospital Finder Automation

## Overview

This repository contains the completed automation solution for the QEA SDET C# Capstone Project.

The project is based on the Practo website and demonstrates a range of functional and non-functional testing techniques covered during the technical training.

The solution combines Playwright, Selenium WebDriver, Reqnroll BDD, accessibility testing, performance testing, security testing, reporting, logging and Docker-based execution.

---

## Project Requirements

The main scenarios covered are:

### 1. Hospital Finder

Find hospitals in Bangalore that:

- Are open 24/7.
- Have a rating greater than 3.5.
- Have a parking facility.
- Display the matching hospital names.

The automation extracts hospital information from the search results, identifies candidate hospitals and validates parking information from the hospital details page.

### 2. Diagnostics

Navigate to the Diagnostics page and:

- Extract the displayed top city names.
- Store the city names in a collection.
- Display and validate the collected city information.

### 3. Corporate Wellness

Navigate to Corporate Wellness and:

- Enter invalid details into the form.
- Attempt to submit the request.
- Capture and validate the resulting warning or validation behaviour.

---

## Automation Approach

The solution demonstrates:

- C# and .NET
- NUnit
- Playwright
- Selenium WebDriver
- Reqnroll BDD
- Gherkin feature files
- Page Object Model (POM)
- Explicit waits and reliable element interaction
- Search and navigation
- Form handling
- Multiple browser windows/tabs
- Collection and extraction of multiple web elements
- Positive and negative testing
- Parallel test execution
- Failure screenshots and test evidence
- Logging
- Quality gates

Playwright is used for the primary functional automation and accessibility testing.

Selenium WebDriver with Reqnroll is used to demonstrate BDD automation and Docker Selenium Grid execution.

---

## Accessibility Testing

Accessibility testing is implemented using Playwright and axe-core.

The accessibility coverage includes:

- Automated axe accessibility scanning.
- Reporting discovered accessibility violations.
- Classification of violations by impact.
- A quality gate for serious and critical violations.
- Keyboard-only navigation testing.
- Validation of logical keyboard focus order through the Corporate Wellness form.

The automated axe scan reports all detected violations while failing the quality gate if serious or critical accessibility violations are found.

---

## Performance Testing

Apache JMeter is used to demonstrate performance testing against an appropriate Practo journey.

The performance test captures metrics including:

- Response time.
- Throughput.
- Error rate.

The project includes:

- JMeter test plan.
- Raw execution results where appropriate.
- Summary and aggregate results.
- Response-time evidence.
- Generated performance reporting artefacts.

---

## Security Testing

OWASP ZAP is used to demonstrate passive web security testing.

The security solution includes:

- Browser traffic routed through the ZAP proxy.
- Passive scanning of captured traffic.
- Collection and classification of security alerts.
- Automated security assertions and quality-gate behaviour.
- JSON and HTML reporting artefacts.

A passive approach is used because the target is the public Practo website.

---

## Docker and Selenium Grid

Docker is used to demonstrate containerised test execution.

The project includes:

- Selenium Grid running in Docker.
- RemoteWebDriver execution.
- Containerised Chrome browser execution.
- noVNC browser observation.
- Docker Compose configuration.

This demonstrates that the Selenium BDD framework can execute against a remote browser rather than relying only on a locally installed browser.

---

## Reporting and Evidence

The solution includes multiple forms of test reporting and evidence:

- Allure Reports.
- Extent Reports.
- NUnit console output.
- Failure screenshots.
- Serilog logging.
- JMeter performance results.
- OWASP ZAP security reports.
- Recorded test demonstrations.

Extent reporting is integrated with the Selenium BDD scenarios, while Allure is used as additional execution evidence.

---

## Test Documentation

Supporting QA documentation is stored in the `Documentation` directory.

This includes:

- Test Plan.
- Test Cases.
- Test Data.
- Requirements Traceability Matrix (RTM).

The documentation provides traceability between the project requirements and the implemented automated tests.

---

## Solution Structure

The repository is organised into the following main components:

- `Practo.HospitalFinder.Playwright`
  - Playwright functional automation.
  - Page Object Model.
  - Accessibility testing.
  - Failure evidence.

- `Practo.HospitalFinder.SeleniumBDD`
  - Selenium WebDriver automation.
  - Reqnroll BDD.
  - Gherkin feature files.
  - Extent reporting.
  - Serilog logging.
  - Docker Selenium Grid execution.

- `Practo.HospitalFinder.Security`
  - OWASP ZAP passive security automation.
  - Security reports and alert evidence.

- `Performance`
  - Apache JMeter test plan.
  - Performance results and supporting evidence.

- `Documentation`
  - Test Plan.
  - Test Cases.
  - Test Data.
  - Requirements Traceability Matrix.

- `docker-compose.yml`
  - Selenium Grid Docker configuration.

---

## Git and GitHub Workflow

The project was developed using a structured Git workflow.

The workflow included:

- Feature branches for major stages of development.
- Meaningful commits.
- `git status` and `git diff` checks.
- Remote branches on GitHub.
- Pull requests.
- Merging completed features into `main`.
- Final integration and verification.

Example feature areas included:

- Reporting and evidence.
- Parallel execution.
- Accessibility testing.
- Performance testing.
- Security testing.
- Docker Selenium Grid.
- Final integration.

---

## Project Status

**Complete – ready for capstone demonstration and presentation.**

The final solution demonstrates functional automation alongside accessibility, performance and security testing, supported by reporting, logging, Docker execution, QA documentation and a structured Git workflow.