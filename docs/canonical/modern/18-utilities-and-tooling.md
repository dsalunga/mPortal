# 18 - Modern Utilities And Tooling

## Purpose
Reframe legacy desktop/utility tools into modern, governable operational tooling.

## Target Direction
- Prefer internal web/CLI operational tools with RBAC and audit logs.
- Retire or minimize Windows-only desktop utilities where feasible.
- Expose automation-friendly interfaces for backup/deploy/maintenance tasks.

## Migration Approach
- Classify each utility: keep, replace, or retire.
- Implement high-value replacements first (DB ops, deploy orchestration, extract/report paths).
- Document ownership and support model per retained utility.

## Control Requirements
- All privileged operations require identity-backed authorization.
- Audit events and execution logs retained for traceability.
- Operational scripts validated through CI where possible.

## Legacy Reference
- [Legacy card baseline](../legacy/18-utilities-and-tooling.md)
