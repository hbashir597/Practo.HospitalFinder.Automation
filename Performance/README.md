# Performance Testing

## Overview

This component contains the performance testing carried out for the Practo Hospital Finder capstone project using Apache JMeter.

The performance scenario focuses on the Corporate Wellness page, which forms part of Requirement 3 of the project.

The purpose of the test is to capture key performance metrics including response time, throughput and error rate.

## Tool

- Apache JMeter 5.6.3

## Test Scenario

The test sends HTTP GET requests to the Practo Corporate Wellness page:

`https://www.practo.com/plus/corporate`

The request was identified using the JMeter HTTP(S) Test Script Recorder and added to the Thread Group for execution.

## Load Configuration

The final test was configured with:

- Virtual Users (Threads): 50
- Ramp-Up Period: 10 seconds
- Loop Count: 10
- Total Samples: 500

This means 50 virtual users executed the request 10 times each, producing 500 samples.

## Results

The final test run produced the following results:

| Metric | Result |
|---|---:|
| Total Samples | 500 |
| Average Response Time | 695 ms |
| Median Response Time | 631 ms |
| 90th Percentile | 1127 ms |
| 95th Percentile | 1281 ms |
| 99th Percentile | 1770 ms |
| Minimum Response Time | 219 ms |
| Maximum Response Time | 2402 ms |
| Throughput | 29.2 requests/second |
| Error Rate | 0.00% |

## JMeter Listeners

The following JMeter listeners were used to inspect the test execution and results:

- View Results Tree
- Summary Report
- Aggregate Report
- Response Time Graph

The Aggregate Report was used to analyse response-time percentiles, throughput and error rate.

The Response Time Graph was configured with a 1000 ms interval to provide a clearer view of how response time changed during the test run.

## Evidence

The `results` directory contains the evidence captured from the final performance test:

- `corporate-wellness-summary.csv`
- `corporate-wellness-aggregate.csv`
- `corporate-wellness-response-time.png`

The JMeter test plan is stored as:

- `PractoHospitalFinderPerformance.jmx`

## Notes and Limitations

Practo is a third-party public website. The performance test was therefore kept controlled and was performed for training and demonstration purposes.

The results represent observations from this specific test execution and should not be interpreted as a full capacity assessment or production Service Level Agreement (SLA) for Practo.

This JMeter scenario measures HTTP request performance for the Corporate Wellness page rather than the complete browser rendering experience.