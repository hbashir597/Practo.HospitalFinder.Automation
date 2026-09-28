# Requirements Traceability Matrix - Practo Hospital Finder

## Core Functional Requirements

| Requirement | Description | Test Case | Automation |
|---|---|---|---|
| REQ-01 | Find qualifying hospitals in Bangalore that are open 24/7, have parking available and have a rating greater than 3.5 | TC-01 | Playwright |
| REQ-02 | Extract, store and display the top cities from Diagnostics | TC-02 | Playwright |
| REQ-03 | Enter invalid Corporate Wellness details and validate the resulting warning or validation behaviour | TC-03 | Playwright |

## Additional Automation Coverage

| Coverage ID | Objective | Test Case | Tool / Framework |
|---|---|---|---|
| COV-01 | Demonstrate BDD-based functional automation | TC-04 | Selenium + Reqnroll |
| COV-02 | Perform automated accessibility scanning and apply an accessibility quality gate | TC-05 | Playwright + axe-core |
| COV-03 | Validate logical keyboard focus order | TC-06 | Playwright |
| COV-04 | Perform controlled performance testing and capture performance metrics | TC-07 | Apache JMeter |
| COV-05 | Perform passive security testing and evaluate security findings | TC-08 | OWASP ZAP |
| COV-06 | Demonstrate remote/containerised browser execution | TC-09 | Docker + Selenium Grid |

## Supporting Framework Capabilities

| Capability | Implementation |
|---|---|
| Page Object Model | Playwright and Selenium automation frameworks |
| BDD | Reqnroll + Gherkin |
| Parallel Execution | NUnit configuration |
| Test Reporting | Allure + Extent Reports |
| Failure Evidence | Automated screenshots |
| Logging | Serilog |
| Performance Evidence | JMeter results and reports |
| Security Evidence | OWASP ZAP alerts and reports |
| Containerised Execution | Docker + Selenium Grid |
| Version Control | Git feature-branch workflow and GitHub pull requests |

## Traceability Summary

All three core functional requirements are covered by automated tests.

Additional test coverage demonstrates the wider quality-engineering techniques used in the project, including BDD, accessibility, performance, security and containerised execution.

This provides traceability between the original functional requirements and the additional technical objectives demonstrated as part of the capstone project.