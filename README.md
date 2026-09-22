# Practo Hospital Finder Automation

## Overview

This repository contains the automation solution for the QEA SDET C# Capstone Project.

The project is based on the Practo website and demonstrates functional and non-functional testing techniques covered during the technical training.

## Project Requirements

The main scenarios are:

1. Find hospitals in Bangalore that:
   - Are open 24/7.
   - Have a parking facility.
   - Have a rating greater than 3.5.
   - Display the matching hospital names.

2. Navigate to the Diagnostics page:
   - Extract the top city names.
   - Store the city names in a collection.
   - Display the collected city names.

3. Navigate to Corporate Wellness:
   - Enter invalid details into the form.
   - Attempt to schedule the request.
   - Capture and validate the warning message.

## Test Automation Approach

The solution will demonstrate:

- C# and .NET
- NUnit
- Playwright
- Selenium WebDriver
- Reqnroll BDD
- Gherkin feature files
- Page Object Model
- Explicit waits and reliable element interaction
- Search and navigation
- Form handling
- Alert and warning message handling
- Multiple browser windows where applicable
- Collection and extraction of multiple web elements
- Parallel test execution
- Positive and negative testing
- Screenshots and test evidence
- Logging where appropriate

## Reporting

Test execution evidence will be provided using:

- Allure Reports
- Extent Reports

## Performance Testing

Apache JMeter will be used to demonstrate performance testing and capture metrics including:

- Response time
- Throughput
- Error rate

## Accessibility Testing

Accessibility testing will include:

- Automated accessibility scanning
- Keyboard-only accessibility checks
- Screen-reader compatibility considerations
- Identification and reporting of accessibility violations

## Security Testing

OWASP ZAP will be used for basic web security testing.

Testing against the public Practo website will use a passive security testing approach.

## Docker

Docker and Selenium Grid will be used to demonstrate remote/containerised browser execution.

## Test Documentation

The project will include supporting QA documentation such as:

- Test scenarios and test cases
- Test data
- Positive and negative coverage
- Requirements Traceability Matrix (RTM)
- Defect evidence where applicable

## Git and GitHub

The project follows a structured Git workflow using:

- Feature branches
- Meaningful commits
- Git status and diff checks
- GitHub remote branches
- Pull requests
- Merging completed work into `main`

## Solution Structure

- `Practo.HospitalFinder.Playwright` - Playwright-based functional automation.
- `Practo.HospitalFinder.SeleniumBDD` - Selenium WebDriver automation using Reqnroll BDD.
- Additional folders for performance, security, test documentation and supporting artefacts will be added as the project develops.

Each major project or testing component will contain its own README where appropriate.

## Project Status

Project setup is currently in progress.