# Practo Hospital Finder - Playwright

## Purpose

This project contains the Playwright-based functional test automation for the Practo Hospital Finder capstone project.

Playwright is used for the primary functional user journeys against the Practo website.

## Technology

- C#
- .NET
- NUnit
- Microsoft Playwright
- Page Object Model

## Planned Coverage

The Playwright test suite will cover relevant functional scenarios from the project specification, including:

- Hospital search functionality.
- Hospital filtering and result validation where supported by the current Practo website.
- Diagnostics page navigation and extraction of top city names.
- Corporate Wellness form interaction and validation.
- Navigation between relevant pages.
- Collection and validation of multiple web elements.
- Positive and negative test scenarios.

## Framework Design

The framework will use:

- Page Object Model
- Reusable page objects and components
- Configuration management
- Reliable Playwright locators
- Playwright assertions
- Parallel execution where appropriate
- Screenshots and other test evidence
- Allure reporting

## Current Status

Playwright and NUnit have been configured successfully.

A smoke test has been created and executed successfully to verify the Playwright setup.

Further framework components and functional tests will be added as the project develops.