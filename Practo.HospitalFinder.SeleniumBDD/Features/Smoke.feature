Feature: BDD framework smoke test

  Scenario: Verify the BDD framework is working
    Given the BDD framework is configured
    When the smoke scenario is executed
    Then the scenario should pass