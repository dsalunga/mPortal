# Modern Migration Target Decisions (.NET 10)

This document records the migration target choices for the legacy modernization program.

Decision policy for this version:
- Primary objective: minimize recurring license cost while preserving security, maintainability, and migration safety.
- Target runtime baseline: `.NET 10`.
- Default database target: `PostgreSQL`.
- Scope: decisions for platform, identity, editor, jobs, data migration, CI/CD, and operational tooling.
- Third-party rule: decisions requiring third-party tools/apps are marked `Deferred` by default, unless they are blockers for a migration phase.

Decision state legend:
- `Picked (Confirmed)`: selected and confirmed.
- `Picked (Recommended)`: selected as baseline recommendation; owner sign-off still needed.
- `Deferred`: intentionally deferred until the relevant phase.
- `Do Not Migrate As-Is`: validated non-lift-and-shift area.

## Approval Status

Owner accepted the detailed recommendations in this document on `2026-04-15`.

## Execution Constraint (Owner Confirmed)

- `GitHub Actions` remains the selected CI/CD platform, but workflows stay **disabled for now** in the private repository.
- This is not a blocker for module migration and local validation.
- This becomes a blocker at controlled release/cutover time, when pipeline governance and promotion controls are required.

## Decision Table

| Area | Options Considered | Picked Choice | Why Picked | Requires Third-Party Tool/App | Timing | Blocker | Decision State |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Runtime/Web Platform | `ASP.NET Core 10`; retain legacy ASP.NET/WebForms; mixed dual runtime | `ASP.NET Core 10` on `.NET 10` | Long-term supportability, performance, security posture, and cleaner modernization path | No | Active | No | Picked (Confirmed) |
| Architecture Migration Pattern | Big-bang rewrite; staged strangler; keep legacy core as-is | Staged strangler migration by bounded capability | Lowers risk, supports incremental cutover, easier rollback | No | Active | No | Picked (Confirmed) |
| CMS Module Manifest Contract | DB-only manifest; code-only manifest; hybrid DB + code manifest | Hybrid DB + code manifest | Preserves dynamic CMS composition while adding compile-time validation and safer startup registration | No | Active | No | Picked (Confirmed) |
| Admin Modernization UI Strategy | Razor-first; API + SPA-first; hybrid per domain | Hybrid per domain | Balances migration speed and maintainability: Razor-first for core admin/CMS, API + SPA for high-interactivity workflows | No | Active | No | Picked (Confirmed) |
| Identity and Authentication | `ASP.NET Core Identity + OpenIddict`; `Duende IdentityServer`; `Keycloak` (self-hosted); `Microsoft Entra`; `Auth0` | `ASP.NET Core Identity + OpenIddict` | Fully .NET-native path, no external IdP platform dependency required, strong fit for phased legacy migration | Yes (NuGet packages) | Active | No (selection closed) | Picked (Confirmed) |
| Rich Text Editor | `CKEditor 5`; `TinyMCE`; `TipTap` | `TipTap OSS` + server-side HTML sanitization | Free-first licensing clarity and modern integration flexibility, with a single explicit replacement target across all legacy editor surfaces | Yes (OSS package) | Active | No (selection closed) | Picked (Confirmed) |
| Background Jobs/Scheduling | `Quartz.NET`; `Hangfire Core`; custom worker-only scheduler | `Quartz.NET` + .NET Worker services | .NET-native, durable scheduling, predictable ops model | Yes (OSS package) | Active | No (selection closed) | Picked (Confirmed) |
| Integration Endpoints (ASMX/WCF/ASHX) | Direct lift-and-shift; full rewrite first; API adapter strangler | API adapter strangler (`REST/JSON`) + endpoint-by-endpoint migration | Safest path for compatibility and progressive replacement | No | Active | No (pattern can start now) | Picked (Confirmed) |
| Database Migration Strategy | `EF Core Migrations + DbUp`; `Flyway Community`; `Liquibase Community`; `EF Migrations` only | `EF Core Migrations + DbUp` hybrid (`PostgreSQL` default via `DbUp.Postgresql`) | .NET-native migration toolchain with SQL-first support where needed | Yes (OSS packages/tools) | Active | No (selection closed) | Picked (Confirmed) |
| CI/CD Pipeline | `GitHub Actions`; `Azure DevOps`; self-hosted `Jenkins/GitLab` | `GitHub Actions` with strict usage guardrails (**disabled for now**) | Native fit for GitHub repo workflows and fastest adoption | Yes (external platform) | Deferred (disabled until release phase) | Yes: blocker for controlled release/cutover | Picked (Confirmed) |
| Build/Release Orchestration | Keep NAnt + junction scripts; replace with modern pipelines | Replace with deterministic pipeline build + artifact promotion | Repeatable releases, lower operational risk, auditable process | Indirect (depends on CI/CD platform) | Deferred (paired with CI/CD activation) | Yes: blocker for release/cutover governance | Picked (Confirmed) |
| Legacy Desktop Utilities (Deploy/DB editors) | Migrate desktop apps as-is; retire; replace with internal web/CLI tools | Replace with internal web/CLI ops tooling + RBAC + audit logging | Better governance and supportability, lower desktop fragility | No (can be in-house) | Active | No | Picked (Confirmed) |
| Observability | Minimal logging only; paid APM-first; OSS observability stack | OpenTelemetry + Prometheus + Grafana/Loki baseline | Free-first and sufficient for migration reliability gates | Yes (OSS stack) | Deferred (readiness phase) | Yes: blocker before UAT/prod cutover | Picked (Confirmed) |

## Reference Alternatives (Not Selected)

This section preserves previously considered options for traceability.

| Area | Not-Selected Alternatives |
| --- | --- |
| Identity | `Duende IdentityServer`, `Keycloak`, `Microsoft Entra`, `Auth0` |
| CMS module manifest | DB-only manifest, code-only manifest |
| Admin modernization strategy | Razor-first, API + SPA-first |
| Editor | `CKEditor 5`, `TinyMCE` |
| Jobs | `Hangfire Core`, custom worker-only scheduler |
| Database migration | `Flyway Community`, `Liquibase Community`, `EF Migrations` only |
| CI/CD | `Azure DevOps`, self-hosted `Jenkins/GitLab` |

## Editor Cutover Rule (Mandatory)

1. `FredCK.FCKeditorV2` is **Do Not Migrate As-Is**.
2. Legacy `FCKeditor`/`CKEditor.ascx` surfaces are replacement-only targets.
3. All editor usage in the modern stack must converge to `TipTap OSS` + server-side HTML sanitization.
4. No new migration work should attempt `FCKeditor` lift-and-shift or `CKEditor 5`/`TinyMCE` adoption for this program.

## Deferred Third-Party Decisions And Blocker Gates

| Area | Deferred? | Blocks Which Phase? | Earliest Latest-Safe Decision Point |
| --- | --- | --- | --- |
| CI/CD (GitHub Actions) | Yes (workflows disabled now) | Controlled release and final cutover | Before card `15` release pipeline hardening and first staging release |
| Build/Release orchestration | Yes | Release governance and cutover workflow | Before card `15` execution and release rehearsal |
| Observability stack | Yes | UAT/prod readiness gate | Before cutover readiness and production go-live checklist |

Current assessment:
- Decision-level blockers for core `.NET 10` module migration are closed.
- Remaining blockers are release/readiness phase gates (`15` + UAT/prod).

## Validated Non-Lift-And-Shift Areas

| Legacy Area | Migration Decision | Target Direction |
| --- | --- | --- |
| Legacy auth/session model | Do Not Migrate As-Is | Modern .NET identity/session model via ASP.NET Core Identity + OpenIddict with secure secret handling and password rehash migration path |
| ASMX/WCF/ASHX transport contracts | Do Not Migrate As-Is | Adapter/strangler APIs in modern endpoints, then progressive client cutover |
| Thread-abort agent/service model | Do Not Migrate As-Is | Durable scheduling and worker execution model (Quartz.NET + workers) |
| NAnt + junction release flow | Do Not Migrate As-Is | CI/CD workflows with deterministic builds and artifact promotion |
| FCKeditor/VS2003 legacy artifacts | Do Not Migrate As-Is | Do not migrate legacy editor binaries or controls; replace all usage with TipTap OSS and archive obsolete compatibility artifacts |
| Vendored sample/demo assets | Do Not Migrate As-Is | Exclude from migration scope unless explicitly business-used |

## Guardrails For Free-First Execution

1. Prefer OSS/community editions by default; document any commercial exception before adoption.
2. Add license/compliance review gates for every third-party package before production use.
3. When GitHub Actions is enabled, track CI minutes/storage monthly to avoid accidental cost growth in private repos.
4. Keep paid-upgrade triggers explicit (scale, compliance, support SLA, enterprise features).

## Detailed Decision Context And Recommendations

### 1) Legacy User Migration Into ASP.NET Core Identity

| Option | How It Works | Pros | Cons | Recommendation |
| --- | --- | --- | --- | --- |
| Rehash-on-login only | Keep legacy hash verifier; when user logs in successfully, rehash into ASP.NET Core Identity format | Safest cryptographic handling (no plaintext export), low operational complexity | Dormant users may never migrate; long tail remains | Use as baseline mechanism |
| Config-driven forced reset campaign | Optional campaign mode controlled by system config | Gives incident/compliance response capability when needed | User friction if enabled broadly | Keep available but disabled by default |
| Hybrid (recommended) | Rehash-on-login baseline + optional config-driven forced reset (off by default) | Balances low-friction UX with security override flexibility | Requires clear configuration governance | **Recommended** |

Recommended execution pattern:
1. Implement legacy hash verification in a custom ASP.NET Core Identity password hasher path.
2. Rehash automatically on successful login.
3. Keep forced-reset campaigns disabled by default (`InactivePasswordResetEnabled = false`).
4. Enable forced-reset campaigns only for explicit security/compliance events.
5. Set a final legacy-hash decommission date based on accepted risk posture and migration coverage.

### 2) EF Core Migrations vs DbUp Ownership Boundaries

Core rule: one DB object has one owner tool to avoid drift.

| Domain / Schema Area | Primary Owner | Secondary Use | Notes |
| --- | --- | --- | --- |
| Identity (`AspNet*`, OpenIddict tables) | `EF Core Migrations` | None | Keep fully code-first and versioned in auth host |
| Core CMS metadata (sites, pages, modules, settings) | `EF Core Migrations` | `DbUp` for exceptional hotfix/data repair scripts | Prefer EF for long-term maintainability |
| Business module transactional tables | `EF Core Migrations` | `DbUp` for one-off data backfills | Keep model ownership near module code |
| Stored procedures/functions/views/triggers | `DbUp` | None | SQL-first assets are clearer in script form |
| Reporting/index/search support objects | `DbUp` | None | Usually SQL-heavy and environment-sensitive |
| Cross-schema data migrations and large transforms | `DbUp` | None | Safer control for long-running/ordered SQL scripts |

Implementation notes:
- `DbUp` PostgreSQL support is via `DbUp.Postgresql`.

Ownership guardrails:
1. Do not let EF create/alter objects that are DbUp-owned.
2. Maintain the canonical ownership registry at `docs/legacy-migration/database/ef-dbup-ownership-registry.md`.
3. Optional machine-readable map path: `Database/migration-ownership/ownership-map.yaml`.
4. Maintainers: platform/data migration owner (primary), plus required review from affected module owner and DB reviewer.
5. Run drift checks in CI once GitHub Actions is enabled.

### 3) Observability SLO Gates Before Production Cutover

Why this is a gate: migration without operational SLO guardrails can pass functional tests but still fail under real traffic and background-load behavior.

Recommended baseline by environment:

| Environment | Availability | Error Rate | Latency | Job Success | Gate Type |
| --- | --- | --- | --- | --- | --- |
| Dev | Track-only | Track-only | Track-only | Track-only | No hard gate |
| QA | N/A (functional lane) | `5xx < 2%` | `P95 < 1000ms` (critical paths) | `>= 97%` | Advisory gate |
| UAT/Staging | `>= 99.5%` (soak) | `5xx < 1%` | `P95 < 500ms` core, `P95 < 1200ms` heavy admin | `>= 99%` | Cutover gate |
| Production (stabilized target) | `>= 99.9%` | `5xx < 0.5%` | Same as staging or stricter per domain | `>= 99%` | Operational SLO |

Additional mandatory gate:
- Migration reliability: `100%` successful rehearsal runs for upgrade + rollback in non-prod.
- Alerting readiness: critical alerts routed with actionable runbooks and fast detection paths.

Interpretation:
- Not a blocker for ongoing code migration.
- Blocker for UAT exit and production cutover approval.

### 4) Hybrid Admin Strategy (Domain-by-Domain Split)

Recommended default split:

| Admin Domain | Default UI Mode | Why | Suggested Priority |
| --- | --- | --- | --- |
| Site/Page/Template administration | Razor-first | CRUD-heavy, form workflows, low client-state complexity | Phase 1 |
| User/Role/Permission administration | Razor-first | Security-sensitive, predictable workflows | Phase 1 |
| Content moderation/review queues | Razor-first | Fast migration with strong auditability | Phase 1 |
| Media library manager (bulk upload, drag-drop, preview) | API + SPA-first | High interactivity and client-side UX value | Phase 2 |
| Dashboard/analytics and live operational panels | API + SPA-first | Data-rich visual interactions, polling/streaming patterns | Phase 2 |
| Bulk operations/import-export tooling | API + SPA-first | Better async UX and progress handling | Phase 2 |
| Audit/log browsing and diagnostics | Razor-first (with API endpoints behind it) | Fast delivery and lower frontend complexity | Phase 1 |
| Job scheduling/monitoring console | API + SPA-first | Better real-time state updates and filtering UX | Phase 2 |

Recommended rollout posture:
1. Start with Razor-first for core admin governance domains.
2. Limit SPA-first to interaction-heavy domains where it gives clear UX/ops benefit.
3. Keep API contracts reusable so Razor and SPA surfaces can evolve independently.

## Approved Baseline Snapshot (Owner Confirmed)

1. Legacy account migration method:
`Hybrid` baseline accepted (`rehash-on-login` + optional config-driven forced reset), with forced reset disabled by default and enabled only for explicit security/compliance events.
2. EF Core vs DbUp ownership:
Single-owner-per-object policy accepted (EF for entity-owned schemas/tables; DbUp for SQL-native artifacts and cross-schema transforms), with registry at `docs/legacy-migration/database/ef-dbup-ownership-registry.md`.
3. CI/CD constraint:
`GitHub Actions` remains selected but disabled in private repo for now; enablement deferred to release hardening phase.
4. Observability cutover gates:
Per-environment SLO baseline model accepted as mandatory before UAT/prod cutover.
5. Hybrid admin split:
Domain split accepted (Razor-first for core governance workflows; API + SPA-first for interaction-heavy domains).
6. Default database:
`PostgreSQL` accepted as default runtime database target, with SQL Server maintained as compatibility lane where needed.

## Remaining Inputs (Non-Blocking Implementation Details)

1. Final per-environment SLO thresholds for each release tier (can start from recommended defaults).
2. If/when forced-reset campaign mode is ever enabled, define the event trigger, window, and comms runbook.
3. Optional: add machine-readable ownership map at `Database/migration-ownership/ownership-map.yaml`.

## Next Step

Reference this approved baseline from migration execution checklists and per-solution tracking docs, then proceed implementation by phase.
