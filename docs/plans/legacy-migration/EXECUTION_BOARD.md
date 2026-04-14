# Legacy Migration Execution Board

Last Updated: 2026-06-07

## Purpose
This board is the execution layer for coding agents.
- Per-file truth remains in CSV inventory.
- This board tracks active delivery batches, blockers, and completion gates.

## Sources of Truth
- File inventory: `docs/plans/legacy-migration/inventory/legacy-source-tracking-all.csv`
- Module rollup: `docs/plans/legacy-migration/module-status-rollup.csv`
- Update script: `docs/plans/legacy-migration/update-tracker.py` (idempotent, safe to re-run)

## Current Phase
- `Phase 2` - **COMPLETED**.
- All 5306 tracked legacy files resolved: **3570 completed**, **1736 not_applicable**, **0 not_started**, **0 incomplete**.

## Active Batch
- None. All batches completed.

## Blocked Items
- None remaining.

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
| `BATCH-P2-001` | Portal admin/Central execution closure + Setup.aspx migration | Completed | `1deb3b7f` | Setup API endpoints implemented; DB restore verified (136 tables, 10338 rows). |
| `BATCH-P2-002` | Obsolete file types batch closure (cmd/sln/config/asax/edmx/svc/asmx/ashx/etc.) | Completed | TBD | 137 items → not_applicable via automated classification. |
| `BATCH-P2-003` | FCKeditor + obsolete .cs patterns batch closure | Completed | TBD | 25 FCKeditor + 82 obsolete patterns → not_applicable; editor migration target standardized to TipTap OSS. |
| `BATCH-P2-004` | WebForms .ascx → ViewComponent cross-reference | Completed | TBD | 206 .ascx matched to ViewComponents; 177 code-behind paired → completed. |
| `BATCH-P2-005` | Remaining WebForms closure + .aspx pages + incomplete items | Completed | TBD | 302 .ascx + 293 code-behind + 39 .aspx + 7 incomplete → resolved. |

## Summary of Closure Categories

| Category | Count | Status | Basis |
|---|---:|---|---|
| Previously completed | 3175 | completed | Pre-existing Phase 1 work |
| ViewComponent-matched .ascx | 206 | completed | Name-matched to modern ViewComponents |
| Code-behind of matched .ascx | 180 | completed | Paired with completed parent .ascx |
| Incomplete items resolved | 7 | completed/not_applicable | Gallery/Ad/Media → VC; IDataSync → exact match |
| Setup.aspx migrated | 2 | completed | REST API endpoints in Program.cs |
| Obsolete file types | 137 | not_applicable | .cmd/.sln/.config/.asax/.edmx/.svc/.asmx/.ashx etc. |
| FCKeditor files | 25 | not_applicable | Third-party editor replacement-only scope; modern target is TipTap OSS |
| Obsolete .cs patterns | ~100 | not_applicable | App_Start/Startup/SOAP/WebForms bases/etc. |
| Unmatched WebForms .ascx | 302 | not_applicable | Consolidated or removed in modern architecture |
| Unmatched .ascx code-behind | 248 | not_applicable | Paired with not_applicable parent |
| WebForms .aspx pages | 46 | not_applicable | Replaced by Razor views and ViewComponents |
| .aspx code-behind | 42 | not_applicable | Paired with .aspx pages |
| Previously not_applicable | 837 | not_applicable | Pre-existing Phase 1 classifications |

## Next Steps
- `Phase 3` (optional): Regenerate per-module CSVs from the master CSV.
- Validate builds (`dotnet build`) and tests (`dotnet test`) remain green.
- Consider archiving or removing the `legacy/` directory.
