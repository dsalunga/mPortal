# 10 - Modern SystemParts G3 Modules

## Purpose
Define modernization path for G3 workloads (incident/jobs) and workflow-heavy features.

## Target Architecture
- Expose incident/jobs flows through modern API + component layers.
- Use background workers for asynchronous tasks and notifications.
- Keep workflow state explicit and auditable.

## Migration Focus
- Port high-value user journeys first (create/update/search/report).
- Replace legacy queue/task hooks with Quartz-driven job orchestration where applicable.
- Harden validation and authorization for state-changing endpoints.

## Readiness Criteria
- End-to-end workflow parity tests.
- Operational alerting for job failures and SLA breaches.
- Rollback-safe deployment plan for workflow schema changes.

## Legacy Reference
- [Legacy card baseline](../legacy/10-systemparts-g3-modules.md)
