@ui @regression
Feature: Find suitable hospitals in Bangalore

  As a Practo user
  I want to identify suitable hospitals in Bangalore
  So that I can find hospitals matching my requirements

  Scenario: Find hospitals matching the required criteria
    Given I am viewing hospitals in Bangalore
    When I search the available hospitals
    Then I should find hospitals that are open 24 hours
    And the hospitals should have a rating greater than 3.5
    And the hospitals should have parking available