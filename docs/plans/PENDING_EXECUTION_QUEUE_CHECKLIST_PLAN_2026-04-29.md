# Pending Execution Queue Checklist Plan (2026-04-29)

## Objective

Provide one prioritized queue for all remaining work across .NET 10/PostgreSQL migration validation and security cutover readiness.

## Current Baseline (Validated 2026-04-29)

- `dotnet build mPortal.slnx -v minimal` -> passed (`0 errors`, `0 warnings`)
- `dotnet test mPortal.slnx -v minimal` -> passed (`105 passed`, `10 skipped`, `0 failed`)
- Markdown local-link scan under `docs/` -> `0` missing links

## Priority Rules

- `P0`: blocks migration completion sign-off.
- `P1`: required for production cutover readiness but not a compile/test blocker.
- `P2`: governance/hardening follow-through.

## P0 Queue (Migration Completion Blockers)

- [ ] `P0-01` Execute `CHK-NET10-002` (agent runtime validation against real DB records).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: runtime logs showing scheduled task pickup + successful DB write/read cycle.
- [ ] `P0-02` Execute `CHK-NET10-003` (multi-site host/domain resolution validation).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: test matrix with multiple `WSite` identities resolving expected pages.
- [ ] `P0-03` Execute `CHK-NET10-004` (Central admin end-to-end operational flow validation).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: site/page/template/user CRUD walkthrough with pass/fail report.
- [ ] `P0-04` Execute `CHK-PG-007` (SQL Server -> PostgreSQL parity validation run).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: schema/data parity report plus rollback verification notes.

## P1 Queue (Cutover Readiness)

- [ ] `P1-01` Execute `CHK-NET10-001` (Windows/IIS runtime validation for retained Windows workloads).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: Windows/IIS runbook execution log and endpoint health results.
- [ ] `P1-02` Execute `CHK-NET10-005` (legacy vs modern performance baseline).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: comparative latency/throughput report with workload profile.
- [ ] `P1-03` Execute `CHK-PG-006` (PostgreSQL benchmark runs).
  - Source: `docs/plans/NET10_POSTGRES_REMAINING_CHECKLIST_PLAN.md`
  - Completion evidence: BenchmarkDotNet result artifacts committed or linked from docs.

## P1 Queue (Security Cutover Prerequisites)

- [ ] `P1-04` Complete Phase 0 rotation and invalidation tasks (SMS/DB/crypto/session).
  - Source: `docs/plans/SECURITY_REMEDIATION_PHASED_PLAN.md`
  - Completion evidence: internal rotation record and owner confirmation.
- [ ] `P1-05` Approve history rewrite freeze/comms package (Phase 4 exit criteria).
  - Source: `docs/plans/SECURITY_GIT_HISTORY_REWRITE_RUNBOOK.md`, `docs/plans/SECURITY_REMEDIATION_PHASED_PLAN.md`
  - Completion evidence: approved cutover window and published reset instructions.

## P2 Queue (Governance/Hardening)

- [ ] `P2-01` Complete legacy/vendor artifact classification (`keep`/`sanitize`/`remove`).
  - Source: `docs/plans/SECURITY_REMEDIATION_PHASED_PLAN.md`
  - Completion evidence: approved inventory document and retained-artifact justification.
- [ ] `P2-02` Execute history rewrite cutover and post-cutover full-history scan.
  - Source: `docs/plans/SECURITY_GIT_HISTORY_REWRITE_RUNBOOK.md`
  - Completion evidence: force-push record, consumer reset confirmation, clean post-cutover scan report.
- [ ] `P2-03` Complete formal security sign-off gates.
  - Source: `docs/plans/SECURITY_SECRET_REVIEW_CHECKLIST.md`
  - Completion evidence: security review sign-off + engineering lead sign-off.

## Execution Checklist (Order)

- [ ] Step 1: Close all `P0` items and attach evidence links.
- [ ] Step 2: Close all `P1` migration runtime items and benchmark evidence.
- [ ] Step 3: Close `P1` security prerequisites (rotation + approvals).
- [ ] Step 4: Execute `P2` governance/cutover items.
- [ ] Step 5: Update source plans to checked state and move fully-closed plans to `docs/plans/completed/` only if no pending/deferred items remain.

