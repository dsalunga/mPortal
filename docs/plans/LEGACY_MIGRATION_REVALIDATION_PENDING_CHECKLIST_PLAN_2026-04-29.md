# Legacy Migration Revalidation Checklist Plan (2026-04-29)

## Objective

Validate that legacy migration execution and tracking are fully synchronized, then close all revalidation checklist items with current evidence.

## Validation Summary (2026-04-29)

- Tracker source of truth: `docs/plans/legacy-migration-v1/inventory/legacy-source-tracking-all.csv`
- Current tracker totals: `5306` tracked, `4107` completed, `1199` not_applicable, `0` incomplete, `0` not_started
- Completed-row mapping integrity: `0` missing `migrated_file_1to1`, `0` multi-path `migrated_file_1to1`
- Compatibility/placeholder marker scan (`*.cshtml`):
  - `Migration mode: Compatibility Wrapper` -> `0`
  - `placeholder content.` -> `0`
  - `TODO MIGRATION` -> `0`

## Verification Gates (Executed 2026-04-29)

- Build gate: `dotnet build mPortal.slnx -v minimal` -> passed (`0` errors, `0` warnings)
- Test gate: `dotnet test mPortal.slnx -v minimal` -> passed (`105 passed`, `10 skipped`, `0 failed`)
- Doc-link integrity gate: local markdown link scan under `docs/` -> `0` missing local links

## Checklist Closure

### 1) Program Status Reconciliation

- [x] Reconcile all migration summary totals to CSV truth (`4107/1199/0/0`)
- [x] Remove stale contradictory totals from active status docs
- [x] Keep closure status explicit as `Completed` for current scope

### 2) Tracker Integrity

- [x] Ensure `incomplete=0` and `not_started=0`
- [x] Ensure all `completed` rows have deterministic single `migrated_file_1to1`
- [x] Ensure rollup CSV matches source inventory totals

### 3) Card/Inventory Status Basis Refresh

- [x] Refresh changed per-project status-basis counts in affected LGC cards
- [x] Refresh corresponding rows in `master-inventory-projects.md`
- [x] Refresh corresponding rows in `master-inventory-summary.md`

### 4) Runtime Marker Cleanup Validation

- [x] Validate wrapper marker text has been fully removed
- [x] Validate placeholder marker text has been fully removed
- [x] Confirm no migration TODO markers remain in migrated `.cshtml` files

### 5) Evidence Consistency

- [x] Confirm solution-wide build and tests pass on current workspace
- [x] Confirm docs reference existing local markdown targets
- [x] Align top-level migration status docs with current verification evidence

## Exit Criteria

- [x] CSV tracker status is internally consistent and has no pending file rows
- [x] Active migration docs no longer publish stale totals
- [x] Build/test verification succeeds on the current codebase
- [x] Revalidation checklist has no pending or deferred items

## Notes

- This document is retained as a closure record for the 2026-04-29 revalidation pass.
- If new legacy scope is added later, open a new dated checklist plan instead of reusing this file.
