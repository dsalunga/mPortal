# 11 - Modern Integration Suite And External Services

## Purpose
Move legacy integration endpoints to a robust API-first integration layer.

## Target Integration Model
- Use REST/JSON adapters in front of legacy contracts during migration.
- Define stable integration service interfaces and versioned endpoints.
- Centralize resiliency (retry, timeout, circuit breaker) and schema validation.

## Endpoint Strategy
- ASMX/WCF/ASHX endpoints are replacement targets, not direct ports.
- Perform endpoint-by-endpoint cutover with consumer mapping and compatibility tracking.
- Deprecate old endpoints via staged communication windows and telemetry validation.

## Security And Ops
- Enforce authenticated integration channels and scoped credentials.
- Log request/response envelopes with PII-safe redaction.
- Add contract tests and availability monitors for partner-facing flows.

## Legacy Reference
- [Legacy card baseline](../legacy/11-integration-suite-and-external-services.md)
