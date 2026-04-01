Feature: Hybrid processing orchestration
  To prove the framework is reusable
  As a CMoS automation team
  We want one orchestrated hybrid flow that joins file, API, DB, waiting, and reporting layers

  Scenario: Framework capability demo flow
    Given a reusable CMOS framework scenario context
    When the demo orchestration is prepared
    Then the framework produces manager-readable evidence outputs
