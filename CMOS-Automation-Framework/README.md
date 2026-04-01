# CMOS Automation Framework

This repository is structured as a reusable hybrid automation platform for CMoS rather than a feature-specific test suite. The framework is organized around six reusable layers:

1. Test orchestration layer for end-to-end business scenarios.
2. Channel and file layer for DOEI, SEPDI, SPOT, MISIS, and future pluggable layouts.
3. API and service layer for reusable clients, auth, headers, and negative testing.
4. DB validation layer for IRIS and table-driven CMoS verification.
5. Validation layer for business-readable assertions.
6. Reporting and evidence layer for auditability and manager-readable reporting.

## Target architecture

The codebase now follows this direction under `src` and `tests`:

- `src/API/CMOS` for clients, endpoints, request and response flow, and auth.
- `src/Config` for environment-aware framework settings.
- `src/Constants` for reusable business labels and file types.
- `src/Models` for API, DB, file, validation, and context models.
- `src/Services` for API, DB, file, SFTP, waiters, evidence, reporting, and orchestration.
- `src/Queries` for dedicated SQL packs.
- `src/Validators` for business-readable assertions.
- `src/Builders` for requests, files, and test data.
- `tests/FeatureFiles` and `tests/StepDefinitions` for capability-based test packs.

## Professional pillars

- Modularity: each layer is reusable and can evolve independently.
- Scalability: new APIs, files, tables, and scenarios can be added without redesign.
- Maintainability: configuration, queries, validators, and builders are centralized.
- Hybrid coverage: API, DB, file, SFTP, UI, and async processing can be orchestrated together.
- Auditability: evidence and reports are produced for every orchestrated flow.
- Environment readiness: the framework is designed for LOCAL, DEV3, ST, UAT, and PPROD-style setups.
- Longevity: the solution is positioned as a reusable platform.

## Demo scope

The initial framework demo should prove capability rather than business complexity:

- one reusable API test
- one reusable DB validation test
- one file generation and validation flow
- one orchestrated end-to-end hybrid scenario

The sample hybrid feature under `tests/FeatureFiles/EndToEnd` demonstrates the intended presentation pattern for managers: data prepared, file generated, evidence captured, business validations run, and a report published.
