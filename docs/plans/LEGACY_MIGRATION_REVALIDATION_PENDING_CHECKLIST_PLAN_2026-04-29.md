# Legacy Migration Revalidation Pending Checklist Plan (2026-04-29)

## Objective

Close all remaining legacy migration gaps using current source-of-truth evidence, then align all migration tracking docs to actual status.

## Evidence Snapshot (Revalidation Run: 2026-04-29)

- Tracker source: `docs/plans/legacy-migration-v1/inventory/legacy-source-tracking-all.csv`
- Current status totals: `5306` tracked, `3878` completed, `1195` not_applicable, `233` incomplete, `0` not_started.
- All `233` incomplete rows are `Portal` `.ascx.cs` under `status_basis=codebehind_of_not_applicable`.
- Incomplete distribution:
  - `113` `Portal/WebSystem/WebSystem-MVC`
  - `80` `Portal/WebParts/SystemParts`
  - `29` `Portal/WebParts/SystemPartsG2`
  - `8` `Portal/WebParts/SystemPartsG3`
  - `3` `Portal/WebParts/Integration`
- Incomplete counterpart scan:
  - `213` have direct modern `.cshtml` counterpart.
  - `20` have no direct `.cshtml` counterpart (explicit redesign/replacement decision required).
  - Of the `213` with counterpart: `110` are auto-migrated compatibility wrappers, `103` are non-wrapper modern views.
- Wrapper/stub scan:
  - `145` auto-migrated compatibility wrapper `.cshtml` files exist.
  - `0` wrappers without `Component.InvokeAsync(...)`.
  - `0` wrappers with missing target ViewComponent class.
  - `152` `.cshtml` files still contain migration placeholder markers (`Migration mode: Compatibility Wrapper`, `placeholder content.`, similar markers).
- `legacy-gap-plan.md` parsing:
  - `565` tracked rows, `562` marked `[M]`.
  - `381` `[M]` rows still `not_applicable`.
  - `92` `[M]` rows include re-migrate/re-validated notes and need explicit implementation closure decisions.
- Explicit execution artifact snapshot (generated from CSV):
  - `appendix-a`: `233` incomplete rows with per-row action.
  - `appendix-b`: `20` no-counterpart rows requiring explicit decision.
  - `appendix-c`: `110` wrapper-backed incomplete rows with mapped target component.
  - Wrapper target ambiguity detected in `2` rows (explicitly flagged in appendix-c).
  - `appendix-d`: `152` `.cshtml` files with placeholder/migration markers.
- Build/Test baseline for this revalidation:
  - `dotnet build mPortal.slnx -v minimal`: pass, `0` errors, `20` warnings.
  - `dotnet test Tests/WCMS.WebSystem.IntegrationTests/WCMS.WebSystem.IntegrationTests.csproj -v minimal`: pass (`25 passed`, `10 skipped`, `0 failed`).

## Authoritative Execution Artifacts

Use these as the row-level source of truth for implementation. They are generated from current CSV state and remove batch-level ambiguity.

- `docs/plans/legacy-migration-revalidation-2026-04-29/appendix-a-incomplete-rows-explicit-actions.md`
- `docs/plans/legacy-migration-revalidation-2026-04-29/appendix-b-no-counterpart-decision-list.md`
- `docs/plans/legacy-migration-revalidation-2026-04-29/appendix-c-wrapper-parity-worklist.md`
- `docs/plans/legacy-migration-revalidation-2026-04-29/appendix-d-placeholder-marker-worklist.md`

Tracking fields for day-to-day execution are now included in appendices:

- `Execution Status` (recommended values: `Pending Decision`, `Pending Validation`, `Pending Implementation`, `In Progress`, `Blocked`, `Completed`)
- `Evidence` (commit hash, route/test proof, and mapping/decision rationale)

## Non-Ambiguous Execution Rules

Apply these exact rules for every pending row:

1. If row classification is `counterpart_non_wrapper`, allowed next status is only:
   - `completed` after parity validation evidence is recorded.
2. If row classification is `counterpart_wrapper`, allowed next status is only:
   - `pending` (during parity implementation), then `completed` with ViewComponent/view evidence.
3. If row classification is `no_counterpart`, allowed next status is only:
   - `pending_decision` until explicit decision is made (`rebuild`, `mapped replacement`, or `approved no-migrate`), then update to final status.
4. `not_applicable` is disallowed for `[M]` rows unless an explicit strong blocker/no-migrate rationale is captured in decision notes.
5. `migrated_file_1to1` must be one deterministic modern path for `completed` rows; if not possible, status cannot be `completed`.
6. Any wrapper mapping with `VC Mapping Ambiguous = yes` in appendix-c must be resolved first before implementation is counted complete.

## Ambiguity Register (Current)

The following wrapper mappings still require disambiguation before closure:

- `LEGACY-02457` (`EventViewBasic`) - multiple candidate VC/view targets.
- `LEGACY-02868` (`BodyEvent`) - multiple candidate VC/view targets.

## Key Findings

- Migration is not fully complete yet for tracked file-level closure because `233` rows remain `incomplete`.
- Multiple migration docs currently claim `0 incomplete`; those claims are stale relative to CSV source-of-truth.
- A large subset of modern counterparts are compatibility wrappers and still require feature-equivalence validation or rebuild.
- A subset of rows marked `[M]` in `legacy-gap-plan.md` remain `not_applicable`, which conflicts with execution intent.

## Pending Checklist

### 1) Re-open Program Status And Freeze False-Complete Claims

- [ ] Mark legacy migration status as `In Progress` in all active migration summary docs until checklist exit criteria are met.
- [ ] Add a short "revalidation in progress" banner in:
  - `docs/plans/legacy-migration-v1/README.md`
  - `docs/plans/legacy-migration-v1/EXECUTION_BOARD.md`
  - `docs/plans/legacy-migration-v2/README.md`
- [ ] Remove/replace stale statements that claim `0 incomplete` from all active migration summary docs.

### 2) Resolve `233` Incomplete Tracker Rows

- [ ] Execute all rows in `appendix-a` exactly as listed by `Required Action`.
- [ ] For `103` `counterpart_non_wrapper` rows: validate parity and promote to `completed`.
- [ ] For `110` `counterpart_wrapper` rows: implement parity in target components then promote to `completed`.
- [ ] For `20` `no_counterpart` rows: resolve decisions listed in `appendix-b` and apply final statuses.
- [ ] Ensure every updated row has non-empty `notes`, `status_basis`, and deterministic `migrated_file_1to1` path (or explicit `N/A` with rationale).

### 3) Wrapper And Placeholder Implementation Closure

- [ ] Audit all `145` compatibility wrappers for feature parity against legacy behavior.
- [ ] Resolve `2` ambiguous wrapper target mappings listed in `Ambiguity Register`.
- [ ] Prioritize and implement wrappers tied to active admin/runtime flows first (Central + high-traffic user paths).
- [ ] Replace placeholder component bodies (for example `placeholder content.` rows) with real implementation or explicit retirement decision.
- [ ] For each migrated wrapper/component, capture evidence in tracker (`id`, target file, behavior notes).

### 4) Gap Plan Normalization (`legacy-gap-plan.md`)

- [ ] Normalize `Current Status` semantics so `[M]` rows are not left as `not_applicable` unless explicitly exempted by approved blocker/no-migrate reason.
- [ ] Convert re-migrate-note rows into explicit execution status buckets (`pending`, `in_progress`, `completed`) with consistent column values.
- [ ] Reconcile malformed status values (for example `validated` appearing as status value) into standard status vocabulary.

### 5) Per-Solution/Per-Project Status Refresh

- [ ] Recompute per-project counts from CSV and refresh:
  - `docs/plans/legacy-migration-v2/master-inventory-projects.md`
  - `docs/plans/legacy-migration-v2/master-inventory-summary.md`
  - `docs/plans/legacy-migration-v2/master-inventory-solutions.md`
- [ ] Refresh each affected solution card status basis under `docs/plans/legacy-migration-v2/solutions/**` where counts changed.
- [ ] Keep `FCKeditor` decision unchanged: do not migrate as-is, replacement target remains TipTap.

### 6) Verification Gates Before Re-Declaring Completion

- [ ] Build gate: `dotnet build mPortal.slnx -v minimal` passes with no new warnings introduced by migration work.
- [ ] Test gate: targeted integration and compatibility suites pass (`LegacyEndpointCompatibilityTests` and central CMS path tests).
- [ ] Runtime gate: validate key routes (`/`, `/Central/`, `/Central/Site/*`, `/Central/Security/*`) with seeded DB state.
- [ ] Tracker integrity gate:
  - no `incomplete` rows unless explicitly deferred with rationale,
  - no completed rows with missing/invalid `migrated_file_1to1`,
  - status totals in CSV and migration docs match exactly.

## Exit Criteria

- [ ] `legacy-source-tracking-all.csv` has `0 incomplete` or only explicitly deferred rows with approved rationale.
- [ ] All `[M]` rows in `legacy-gap-plan.md` are either implemented or explicitly exempted with strong blocker/no-migrate rationale.
- [ ] No active migration summary doc claims stale totals.
- [ ] Build/test/runtime verification gates pass and are recorded in docs.

## Notes

- This plan supersedes stale "fully completed" claims for execution purposes.
- Historical closure records remain useful for audit history, but they are not authoritative until tracker/doc totals are reconciled again.
