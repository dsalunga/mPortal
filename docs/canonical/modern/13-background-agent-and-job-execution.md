# 13 - Modern Background Agent And Job Execution

## Purpose
Replace legacy thread-abort and service-host job patterns with durable modern execution.

## Target Job Platform
- Use `Quartz.NET` schedules with `.NET Worker` hosted execution.
- Implement idempotent job handlers with retries and failure classification.
- Track job metadata/state for observability and supportability.

## Migration Strategy
- Map each legacy job to explicit trigger, handler, and dependency contracts.
- Retire direct thread-abort and implicit process-coupled patterns.
- Run shadow mode for critical jobs before final cutover.

## Operational Controls
- Dead-letter/error queue handling for unrecoverable failures.
- Structured logs, metrics, and alerts for execution latency and failure rate.
- Document restart/recovery procedures and ownership runbooks.

## Legacy Reference
- [Legacy card baseline](../legacy/13-background-agent-and-job-execution.md)
