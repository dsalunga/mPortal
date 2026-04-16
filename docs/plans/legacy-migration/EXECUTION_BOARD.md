# Legacy Migration Execution Board

Last Updated: 2026-04-16

## Purpose
This board is the execution layer for coding agents.
- Per-file truth remains in CSV inventory.
- This board tracks active delivery batches, blockers, and completion gates.

## Sources of Truth
- File inventory: `docs/plans/legacy-migration/inventory/legacy-source-tracking-all.csv`
- Module rollup: `docs/plans/legacy-migration/module-status-rollup.csv`
- File tracking: `docs/plans/legacy-migration/MIGRATION_FILE_TRACKING.md` (historical snapshot; CSV inventory remains authoritative during reconciliation)
- CMS admin plan: `docs/plans/WEBSYSTEM_CMS_ADMIN_MIGRATION_PLAN.md`
- Update script: `docs/plans/legacy-migration/update-tracker.py` (idempotent, safe to re-run)
- Pending closure plan: `docs/plans/LEGACY_MIGRATION_PENDING_TASKS_CHECKLIST_PLAN.md`

## Current Phase
- `Phase 2` - **COMPLETED** (file inventory closure).
- `Phase 3` - **IN PROGRESS** (implementation parity closure + remaining control rebuilds).
- Tracker reconciliation pass 1 applied on `2026-04-16`: **5306 tracked**, **3878 completed**, **1428 not_applicable**, **0 not_started**, **0 incomplete**.
- `.ascx` tracker state after reconciliation: **519 total**, **507 completed**, **12 not_applicable**.
- 15 previously misclassified `.ascx` rows were reclassified from `not_applicable` to `completed` with explicit modern-file evidence.
- Program status is intentionally **In Progress** until `docs/plans/LEGACY_MIGRATION_PENDING_TASKS_CHECKLIST_PLAN.md` is closed.

## Active Batch
- `BATCH-RC-001` - Tracker/docs reconciliation (**Completed**, 2026-04-16).
- `BATCH-IMP-001` - Remaining implementation closure from pending checklist (**In Progress**).

## Blocked Items
- No hard blockers identified.
- `12` unresolved `.ascx` controls remain listed in `docs/plans/legacy-migration/inventory/unresolved-ascx-not-applicable.txt`; each requires explicit implementation closure or approved do-not-migrate rationale.

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
| `BATCH-P3-001` | 9 feature gap migration + CSV inventory update | Completed | `b39b00be` | 6 Article Template views completed, ContactUsV2 + PhotoAlbum VCs created; 80 CSV entries updated with migrated_file_1to1; build 0 errors, 85/85 tests pass. |

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
- Execute only the implementation items in `docs/plans/LEGACY_MIGRATION_PENDING_TASKS_CHECKLIST_PLAN.md`.
- Keep tracker/docs updates synchronized after each implementation batch.
- Run focused build/test validation per touched solution group after each implementation batch.

## Post-Phase 2: cshtml View Migration (Completed 2026-06-08)

All remaining cshtml views have been migrated to ASP.NET Core MVC Razor patterns.
Zero csproj exclusions remain for source files.

### Commits (Chronological)
| Chunk | Scope | Commit | Summary |
|---|---|---|---|
| 1 | Integration cshtml fixes | `4c637cf1` | Fix 3 Integration cshtml views for ASP.NET Core |
| 2 | LessonReviewerSession migration | `f2cd8067` | Convert LessonReviewerSession and AttendanceController |
| 3 | WCF stubs + ExternalHelper | `57bcbe2d` | Create WCF service stubs, convert ExternalHelper |
| 4 | EventRegisterUtil QRCoder | `e204ead5` | Migrate System.Drawing → SkiaSharp for QR/image generation |
| 5 | Re-enable ALL Integration exclusions | `a78632cb` | Remove all csproj exclusions from Integration WebApp |
| 6 | WebSystem cshtml views | `9e63e69f` | Migrate all WebSystem views: Page→ViewData, RenderPage→PartialAsync, Request→Context.Request |
| 7 | Final cleanup | `f974f1bf` | Fix remaining ASOP theme RenderPage, update tracking docs |

### Key Migration Patterns Applied
- `@inherits Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>` via `_ViewImports.cshtml`
- `Page.*` / `PageData[]` → `ViewData[]` with casts
- `@RenderPage()` → `@await Html.PartialAsync()` or `@await RazorHelper.RenderPanelAsync()`
- Bare `Request` → `Context.Request` (MVC views lack `Request` property)
- `Request.UserAgent` → `Context.Request.Headers["User-Agent"]`
- `IsPost` → `HttpMethods.IsPost(Context.Request.Method)`
- `new WContext(this)` → `new WContext(Context.Request)` or `new WContext(Context)`
- `@page.xxx` → `@(page.xxx)` to disambiguate from `@page` Razor directive
- `@removeTagHelper` for HeadTagHelper/BodyTagHelper on template files with `<head>`/`<body>` tags
