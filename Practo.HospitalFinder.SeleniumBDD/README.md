# Practo Hospital Finder - Selenium BDD

## Purpose

This project contains the Selenium WebDriver and BDD automation for the Practo Hospital Finder capstone project.

Reqnroll is used to express test scenarios in Gherkin using Given, When and Then steps, with Selenium WebDriver performing the browser interactions.

The main Hospital Finder scenario identifies hospitals in Bangalore that meet the following criteria:

- Open 24 hours.
- Rating greater than 3.5.
- Parking available.

## Technology

- C#
- .NET
- NUnit
- Selenium WebDriver
- Selenium Support
- Reqnroll
- Gherkin
- Page Object Model
- Extent Reports
- Allure
- Docker
- Selenium Grid
- noVNC

## Framework Design

The framework uses:

- Gherkin feature files.
- Reqnroll step definitions.
- Page Object Model.
- Driver management through `DriverFactory`.
- Explicit waits.
- Reusable models and configuration.
- Screenshots on test failure.
- Extent reporting.
- Allure reporting.
- Local and remote Selenium execution.

## Hospital Finder Scenario

The main BDD scenario:

1. Opens the Practo hospital results for Bangalore.
2. Extracts the available hospital information.
3. Identifies hospitals that are open 24x7.
4. Filters hospitals to those with a rating greater than 3.5.
5. Checks the remaining hospital detail pages for parking availability.
6. Reports the hospitals that meet all three criteria.

## Local Selenium Execution

By default, `DriverFactory` creates a local `ChromeDriver`.

Run the UI tests locally with:

```bash
dotnet test Practo.HospitalFinder.SeleniumBDD --filter "TestCategory=ui"
```

## Docker and Selenium Grid

The Selenium BDD framework supports remote browser execution using Docker and Selenium Grid.

Docker Compose creates a Selenium Standalone Chrome container called:

```text
practo-selenium-grid
```

The environment exposes:

- Selenium Grid UI on port `4444`.
- noVNC browser viewing on port `7900`.
- A 1920x1080 browser display.
- 2 GB shared memory for Chrome.
- A 300-second Selenium session timeout.

Start the Selenium environment from the repository root:

```bash
docker compose up -d
```

Verify the container:

```bash
docker ps
```

Selenium Grid can be viewed at:

```text
http://localhost:4444
```

The Chrome session running inside the container can be viewed through noVNC at:

```text
http://localhost:7900
```

## Running the BDD Tests Through Docker

Set `SELENIUM_REMOTE=true` when running the tests:

```bash
SELENIUM_REMOTE=true dotnet test Practo.HospitalFinder.SeleniumBDD --filter "TestCategory=ui" --logger "console;verbosity=detailed"
```

When remote execution is enabled, `DriverFactory` creates a `RemoteWebDriver` instead of a local `ChromeDriver`.

The default Selenium Grid endpoint is:

```text
http://localhost:4444
```

A different endpoint can be supplied using the `SELENIUM_GRID_URL` environment variable.

The execution flow is:

```text
Reqnroll BDD Scenario
        |
        v
DriverFactory
        |
        v
RemoteWebDriver
        |
        v
Selenium Grid
        |
        v
Chrome inside Docker
        |
        v
Practo
```

The Hospital Finder UI scenario has been successfully executed through the Docker Selenium Grid environment.

## Stopping the Selenium Environment

Stop and remove the Compose container and network with:

```bash
docker compose down
```

## Reporting and Evidence

The Selenium BDD framework provides:

- Console test results.
- Extent HTML reporting.
- Allure reporting.
- Failure screenshots.
- BDD scenario output.
- Selenium Grid execution.
- noVNC visual evidence of the remote browser session.

## Current Status

The Selenium BDD framework is operational with both local and Docker-based browser execution.

The Hospital Finder scenario successfully extracts hospital information and validates the required criteria using Selenium, Reqnroll and NUnit.

Docker and Selenium Grid integration has also been successfully verified using the real Hospital Finder UI scenario.