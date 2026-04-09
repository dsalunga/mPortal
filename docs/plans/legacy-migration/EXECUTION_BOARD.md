# Legacy Migration Execution Board

Last Updated: 2026-04-09

## Purpose
This board is the execution layer for coding agents.
- Per-file truth remains in CSV inventory.
- This board tracks active delivery batches, blockers, and completion gates.

## Sources of Truth
- File inventory: `docs/plans/legacy-migration/inventory/legacy-source-tracking-all.csv`
- Module rollup: `docs/plans/legacy-migration/module-status-rollup.csv`

## Current Phase
- `Phase 2` - Execution and parity closure.
- Focus: convert `not_started` and `incomplete` legacy mappings into implemented .NET 10 behavior.

## Active Batch
- `BATCH-P2-001`
- Scope: highest-impact Portal admin/Central routes and components still marked `not_started` or `incomplete`.
- Selection method: prioritize by runtime impact and user-facing admin workflows.

## Blocked Items
- Data parity validation is blocked without a representative production-like dataset in PostgreSQL.
- Some legacy WebForms controls have no deterministic 1:1 replacements and require functional redesign decisions.
- SQL Server specific behaviors still need targeted validation under PostgreSQL for full confidence.

## Done Criteria
A migration batch is done only when all criteria pass:
- All files in the batch have updated CSV status and `migrated_file_1to1` (or explicit `N/A` with reason).
- Replacement behavior compiles and runs on `.NET 10`.
- A smoke-test path is validated for each migrated user-facing route/component.
- Batch has a commit hash and short validation note in the history table.

## Batch History (Last Commit Per Batch)
| Batch ID | Scope | Status | Last Commit | Validation Note |
|---|---|---|---|---|
| `BATCH-P1-TRACKER-BASELINE` | Exhaustive legacy inventory + deterministic mapping cleanup | Completed | `7d86e7c7` | Inventory totals synced; ambiguous mappings reduced to zero. |
| `BATCH-P2-001` | Portal admin/Central execution closure | In Progress | `TBD` | Pending implementation and runtime verification. |

## Next Batch Candidates
- `BATCH-P2-002`: remaining `Portal/WebSystem/WebSystem/Content/Parts/Central/*` parity gaps.
- `BATCH-P2-003`: unresolved `SystemParts` and `SystemPartsG2/G3` UX component replacements.
- `BATCH-P2-004`: post-migration data/DB behavior parity and regression hardening.
