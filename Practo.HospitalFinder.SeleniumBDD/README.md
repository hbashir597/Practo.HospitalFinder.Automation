# Practo Hospital Finder - Selenium BDD

## Purpose

This project contains the Selenium WebDriver and BDD automation for the Practo Hospital Finder capstone project.

Reqnroll is used to express test scenarios in Gherkin using Given, When and Then steps, with Selenium WebDriver performing the browser interactions.

## Technology

- C#
- .NET
- NUnit
- Selenium WebDriver
- Selenium Support
- Reqnroll
- Gherkin
- Page Object Model

## Planned Coverage

The Selenium BDD framework will demonstrate relevant scenarios and automation techniques from the project specification, including:

- BDD-based browser automation.
- Search and navigation.
- Form interaction.
- Warning and alert handling where applicable.
- Multiple browser windows where applicable.
- Extraction of multiple web elements into collections.
- Positive and negative scenarios.
- Parallel test execution.

## Framework Design

The framework will use:

- Gherkin feature files
- Reqnroll step definitions
- Page Object Model
- Driver management
- Explicit waits
- Reusable utilities and configuration
- Screenshots and test evidence
- Logging where appropriate
- Extent reporting

## Docker and Selenium Grid

Selenium tests will later be configured to support remote browser execution using Docker and Selenium Grid.

This will demonstrate containerised test execution and allow browser sessions to be viewed using noVNC where appropriate.

## Current Status

Selenium WebDriver, NUnit and Reqnroll have been configured successfully.

A BDD smoke scenario has been created and executed successfully to verify:

Gherkin feature → Reqnroll → C# step definitions → NUnit

Further Selenium framework components and BDD scenarios will be added as the project develops.