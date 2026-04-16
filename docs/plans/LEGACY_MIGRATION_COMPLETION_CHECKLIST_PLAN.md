# Legacy Migration Completion Checklist Plan

## Objective

Complete migration closure for the **full `legacy/` codebase** with evidence-backed tracking, including replacement of all temporary retired placeholders with migrated equivalents or explicitly approved do-not-migrate decisions.

## Scope

- Source scope: `legacy/**`
- Tracking scope: `docs/legacy-migration/**`, `docs/plans/legacy-migration/**`
- Runtime scope: all modern projects currently mapped from legacy solutions
- Primary immediate hotspot: `legacy/Portal/WebSystem/WebSystem-MVC/Content/Parts/**` and modern compatibility layer

## Completion Criteria

- No migration placeholder wrappers remain in runtime paths for legacy surfaces that are expected to function.
- Every tracked legacy artifact row has one of:
  - `Completed` with modern file/evidence, or
  - `Do Not Migrate As-Is` with approved rationale and replacement target.
- No ambiguous status buckets (`Partial`, `Not Stated`, `Not Started`) in master tracking.
- Inventory and per-solution cards agree with actual source tree.
- Relevant solution builds pass (0 errors).

## Execution Checklist

### A) Runtime Surface Closure (WebSystem + Parts + Themes)

- [x] Replace all `Content/Parts` retired placeholder wrappers with migrated aliases/rebuilt counterparts.
- [x] Replace remaining compatibility placeholder routes for legacy services/handlers under `Content/Parts`.
- [x] Verify all legacy `Content/Parts/*.ascx` and `Content/Themes/*.ascx` resolve to modern runtime targets.
- [x] Validate central/admin loader flows for migrated alias controls.

### B) Repository-Wide Legacy Coverage Audit

- [x] Enumerate all `Not Applicable` rows from `docs/legacy-migration/solutions/**`.
- [x] Reclassify each row to:
  - [x] `Completed (Absorbed Alias)`
  - [x] `Completed (Direct Rebuild)`
  - [x] `Do Not Migrate As-Is` (approved decision only)
- [x] Add/refresh modern evidence path for every reclassified row.

### C) Plan + Tracking Synchronization

- [x] Update `docs/legacy-migration/master-inventory-projects.md` status basis entries where changed.
- [x] Update `docs/legacy-migration/master-inventory-summary.md` status basis entries where changed.
- [x] Update all affected LGC cards under `docs/legacy-migration/solutions/**`.
- [x] Update `docs/plans/legacy-migration/EXECUTION_BOARD.md` and related rollups.

### D) Validation

- [x] Build affected WebSystem solution/project set.
- [x] Build remaining solution groups touched by migration row updates.
- [x] Record verification summary in migration docs.

### E) Delivery

- [ ] Commit source migration changes.
- [ ] Commit docs/tracking synchronization changes.
- [ ] Confirm remaining blockers (if any) are decision-approved and non-ambiguous.

## Workstream Status (This Run)

- [x] Plan initialized
- [x] Runtime placeholder elimination in progress
- [x] Repository-wide `Not Applicable` reclassification in progress
- [x] Tracking sync in progress
- [x] Validation complete
- [ ] Final commit complete

## Notes

- `FCKeditor` artifacts remain `Do Not Migrate As-Is` and must continue to target `TipTap OSS` replacement.
- Existing unstaged local edits outside this migration scope (e.g., `AGENTS.md`) must remain untouched.
