# .NET 10 Migration Gap Analysis (mPortal vs pre-migration copy)

Date: 2026-02-26  
Compared paths:
- Current: `.`
- Pre-migration baseline: `.-copy`

## Scope and method

This analysis is a filesystem + project-structure diff with focused migration parity checks:

1. Project format/target comparison (`.csproj` SDK-style + TFM).
2. File-level delta (added/removed files, grouped by type and module).
3. Legacy WebForms/WCF surface replacement coverage:
   - `.ascx`, `.aspx`, `.asmx`, `.ashx`, `.svc`
   - migration artifacts (`ViewComponents`, `.cshtml`, API controllers).
4. Spot checks for runtime parity risks in current code.

Supporting raw lists are included in this folder.

## Executive summary

- **Platform migration is structurally complete**: all 48 projects are SDK-style and target `.NET 10` (`45 x net10.0`, `3 x net10.0-windows`).
- **Feature migration/parity is not complete**: a large legacy surface was removed, but a subset has no clear replacement yet.
- Highest risk areas are:
  - `Portal/WebParts/SystemParts/SystemParts/AppBundle`
  - `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2`
  - `Portal/WebSystem/WebSystem-MVC/Content/Parts/*` and `Content/Admin/*`

## Quantitative comparison

### Repository-level delta

- Files in current: `6663`
- Files in pre-migration: `8473`
- Files only in pre-migration: `2444`
- Files only in current: `634`

### Project modernization status

- Pre-migration: `48` projects total
  - `47` legacy non-SDK projects
  - `1` SDK-style (`net8.0`)
- Current: `48` projects total
  - `48` SDK-style
  - Targets: `45 x net10.0`, `3 x net10.0-windows`

### Legacy web/service artifacts removed

- `.ascx`: `519`
- `.aspx`: `58`
- `.asmx`: `11`
- `.ashx`: `13`
- `.svc`: `6`
- `Web.config`: `13`
- `packages.config`: `23`
- `.sln`: `19` (replaced by `mPortal.slnx`)

### Modern artifacts added

- `Program.cs` hosts: `8`
- ViewComponent classes added: `270`
- API controller classes added: `7`
- `appsettings*.json` added: `8`

## Area-by-area parity signal

The table below is a signal, not a strict 1:1 proof.

| Area | Old `.ascx` | Old `.aspx` | Current ViewComponents | Current `.cshtml` |
|---|---:|---:|---:|---:|
| `Portal/WebSystem/WebSystem-MVC` | 188 | 33 | 68 | 98 |
| `Portal/WebParts/Integration/IntegrationParts` | 127 | 0 | 130 | 150 |
| `Portal/WebParts/SystemParts/SystemParts` | 136 | 4 | 34 | 37 |
| `Portal/WebParts/SystemPartsG2/SystemPartsG2` | 52 | 3 | 21 | 21 |
| `Portal/WebParts/SystemPartsG3/SystemPartsG3` | 10 | 0 | 10 | 10 |
| `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp` | 4 | 0 | 2 | 10 |
| `BibleReader/BibleReader` | 0 | 7 | 1 | 1 |
| `LessonReviewer/LessonReviewer` | 0 | 3 | 3 | 3 |

Interpretation:
- `IntegrationParts` and `SystemPartsG3` look substantially migrated.
- `SystemParts` and `SystemPartsG2` have lower replacement density and require targeted parity verification.

## Confirmed migration gaps / risks

## 1) Unmatched legacy web artifacts (likely not yet migrated)

- Legacy artifacts removed with no obvious replacement match: `187` files.
- Highest concentrations:
  - `77` in `Portal/WebParts/SystemParts/SystemParts/AppBundle`
  - `34` in `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2`
  - `28` in `Portal/WebSystem/WebSystem-MVC/Content/Parts`
  - `10` in `Portal/WebSystem/WebSystem-MVC/Content/Admin`

See full list: `legacy-unmatched-web-artifacts.txt`

## 2) Service endpoint parity appears partial

Removed legacy service surfaces:
- `11 x .asmx`
- `13 x .ashx`
- `6 x .svc`

Current explicit replacement API controllers are limited (7 classes total), so endpoint-by-endpoint parity still needs a traceability map.

## 3) Non-codegen C# classes removed (potential functional loss)

- Missing `.cs` files (excluding `.designer.cs` and webform code-behind): `99`
- Includes removed provider/helpers and service-client artifacts.

See full list: `missing-csharp-non-codegen.txt`

## 4) Runtime parity indicators in current code

Evidence of interim/scaffold behavior:

- Current host excludes legacy content views:
  - `Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj:10`
- CMS fallback endpoint currently renders a simple stub/404 fallback behavior:
  - `Portal/WebSystem/WCMS.Framework/Middleware/CmsPageEndpointRouteBuilderExtensions.cs:24`
- Pre-migration host was full ASP.NET Framework MVC/WebForms + System.Web stack:
  - `mPortal-copy/Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj:16`
  - `mPortal-copy/Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj:200`

## 5) Plan/status document drift

`NET10_MIGRATION_PLAN.md` currently marks broad completion, but this diff shows meaningful parity work is still pending in multiple modules.

## Migration completion plan (parity-focused)

## Phase 0 — Baseline and traceability (must do first)
- [ ] Create a canonical endpoint inventory from pre-migration:
  - pages (`.aspx`), controls (`.ascx`), handlers/services (`.ashx/.asmx/.svc`), and routes.
- [ ] Create a mapping sheet: `legacy artifact -> modern replacement -> status` (`Migrated`, `Partial`, `Missing`, `Retired by design`).
- [ ] Define acceptance tests per mapped item (UI flow, API contract, auth behavior, data behavior).

## Phase 1 — SystemParts + SystemPartsG2 parity closure
- [ ] Triage `AppBundle` and `AppBundle2` unmatched files first (largest risk bucket: 111 items total).
- [ ] Implement missing ViewComponents/APIs or explicitly retire features with approval.
- [ ] Re-run module smoke tests with real migrated data.

## Phase 2 — WebSystem CMS parity closure
- [ ] Close gaps under `WebSystem-MVC/Content/Parts` and `Content/Admin`.
- [ ] Replace remaining missing central/admin functions (tools, controls, windows, handlers).
- [ ] Replace fallback placeholder rendering with full CMS pipeline parity where still scaffolded.

## Phase 3 — Service endpoint contract parity
- [ ] For each removed `.asmx/.ashx/.svc`, implement modern equivalent or mark retired.
- [ ] Publish contract-diff report (request/response + auth differences).
- [ ] Add integration tests that assert old contract compatibility for required endpoints.

## Phase 4 — Data and environment parity
- [ ] Execute SQL Server -> PostgreSQL data migration for realistic parity testing.
- [ ] Seed required CMS metadata (`WebObject`, `WebSite`, `WebPage`, `WebSiteIdentity`, admin users/roles).
- [ ] Validate key routes (`/`, `/Central`) against populated Postgres.

## Phase 5 — Verification and hardening
- [ ] E2E regression suite across all apps/modules.
- [ ] Performance baseline compare (pre-migration vs .NET 10).
- [ ] Final gap closure report with sign-off per module.

## Recommended immediate next 10 tasks

1. [ ] Freeze this diff snapshot as baseline for parity work.
2. [x] Build the `legacy -> replacement` mapping for all 187 unmatched web artifacts.  
   Output: `legacy-to-modern-mapping.csv` + `MAPPING_EXECUTION_PLAN.md`
3. [x] Prioritize top 30 unmatched artifacts by user/business criticality.  
   Output: `top30-p0-p1-prioritized.csv` + `TOP30_EXECUTION_BACKLOG.md`
4. [ ] Validate `SystemParts/AppBundle` critical flows first.
5. [ ] Validate `SystemPartsG2/AppBundle2` critical flows second.
6. [ ] Validate CMS admin flows under `Content/Admin` and `Content/Parts/Central`.
7. [ ] Complete missing service endpoint replacements (`.asmx/.ashx/.svc` parity matrix).
8. [ ] Apply and test database migration with real dataset.
9. [ ] Update `NET10_MIGRATION_PLAN.md` status from “complete” to evidence-based per-module completion.
10. [ ] Gate production sign-off on passing parity checklist, not project file conversion.

## Supporting artifacts in this folder

- `only-in-pre-migration.txt`
- `only-in-current.txt`
- `legacy-unmatched-web-artifacts.txt`
- `missing-csharp-non-codegen.txt`
- `legacy-to-modern-mapping.csv`
- `MAPPING_EXECUTION_PLAN.md`
- `top30-p0-p1-prioritized.csv`
- `TOP30_EXECUTION_BACKLOG.md`
