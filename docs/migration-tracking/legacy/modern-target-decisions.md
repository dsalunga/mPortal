# Modern Migration Target Decisions (.NET 10)

This document records the migration target choices for the legacy modernization program.

Decision policy for this version:
- Primary objective: minimize recurring license cost while preserving security, maintainability, and migration safety.
- Target runtime baseline: `.NET 10`.
- Scope: decisions for platform, identity, editor, jobs, data migration, CI/CD, and operational tooling.
- Third-party rule: decisions requiring third-party tools/apps are marked `Deferred` by default, unless they are blockers for a migration phase.

Decision state legend:
- `Picked (Confirmed)`: selected and confirmed.
- `Picked (Recommended)`: selected as baseline recommendation; owner sign-off still needed.
- `Deferred`: intentionally deferred until the relevant phase.
- `Do Not Migrate As-Is`: validated non-lift-and-shift area.

## Decision Table

| Area | Options Considered | Picked Choice | Why Picked | Requires Third-Party Tool/App | Timing | Blocker | Decision State |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Runtime/Web Platform | `ASP.NET Core 10`; retain legacy ASP.NET/WebForms; mixed dual runtime | `ASP.NET Core 10` on `.NET 10` | Long-term supportability, performance, security posture, and cleaner modernization path | No | Active | No | Picked (Confirmed) |
| Architecture Migration Pattern | Big-bang rewrite; staged strangler; keep legacy core as-is | Staged strangler migration by bounded capability | Lowers risk, supports incremental cutover, easier rollback | No | Active | No | Picked (Confirmed) |
| Identity and Authentication | `ASP.NET Core Identity + OpenIddict`; `Duende IdentityServer`; `Keycloak` (self-hosted); `Microsoft Entra`; `Auth0` | `ASP.NET Core Identity + OpenIddict` | Fully .NET-native path, no external IdP platform dependency required, strong fit for phased legacy migration | Yes (NuGet packages) | Active | No (selection closed) | Picked (Confirmed) |
| Rich Text Editor | `CKEditor 5`; `TinyMCE`; `Tiptap` | `Tiptap OSS` + server-side HTML sanitization | Free-first licensing clarity and modern integration flexibility | Yes (OSS package) | Active | No (selection closed) | Picked (Confirmed) |
| Background Jobs/Scheduling | `Quartz.NET`; `Hangfire Core`; custom worker-only scheduler | `Quartz.NET` + .NET Worker services | .NET-native, durable scheduling, predictable ops model | Yes (OSS package) | Active | No (selection closed) | Picked (Confirmed) |
| Integration Endpoints (ASMX/WCF/ASHX) | Direct lift-and-shift; full rewrite first; API adapter strangler | API adapter strangler (`REST/JSON`) + endpoint-by-endpoint migration | Safest path for compatibility and progressive replacement | No | Active | No (pattern can start now) | Picked (Confirmed) |
| Database Migration Strategy | `EF Core Migrations + DbUp`; `Flyway Community`; `Liquibase Community`; `EF Migrations` only | `EF Core Migrations + DbUp` hybrid | .NET-native migration toolchain with SQL-first support where needed | Yes (OSS packages/tools) | Active | No (selection closed) | Picked (Confirmed) |
| CI/CD Pipeline | `GitHub Actions`; `Azure DevOps`; self-hosted `Jenkins/GitLab` | `GitHub Actions` with strict usage guardrails | Native fit for GitHub repo workflows and fastest adoption | Yes (external platform) | Deferred (release phase) | Yes: blocker for controlled release/cutover | Picked (Confirmed) |
| Build/Release Orchestration | Keep NAnt + junction scripts; replace with modern pipelines | Replace with deterministic pipeline build + artifact promotion | Repeatable releases, lower operational risk, auditable process | Indirect (depends on CI/CD platform) | Deferred (paired with CI/CD rollout) | Yes: blocker for release/cutover governance | Picked (Confirmed) |
| Legacy Desktop Utilities (Deploy/DB editors) | Migrate desktop apps as-is; retire; replace with internal web/CLI tools | Replace with internal web/CLI ops tooling + RBAC + audit logging | Better governance and supportability, lower desktop fragility | No (can be in-house) | Active | No | Picked (Confirmed) |
| Observability | Minimal logging only; paid APM-first; OSS observability stack | OpenTelemetry + Prometheus + Grafana/Loki baseline | Free-first and sufficient for migration reliability gates | Yes (OSS stack) | Deferred (readiness phase) | Yes: blocker before UAT/prod cutover | Picked (Recommended) |

## Reference Alternatives (Not Selected)

This section preserves previously considered options for traceability.

| Area | Not-Selected Alternatives |
| --- | --- |
| Identity | `Duende IdentityServer`, `Keycloak`, `Microsoft Entra`, `Auth0` |
| Editor | `CKEditor 5`, `TinyMCE` |
| Jobs | `Hangfire Core`, custom worker-only scheduler |
| Database migration | `Flyway Community`, `Liquibase Community`, `EF Migrations` only |
| CI/CD | `Azure DevOps`, self-hosted `Jenkins/GitLab` |

## Deferred Third-Party Decisions And Blocker Gates

| Area | Deferred? | Blocks Which Phase? | Earliest Latest-Safe Decision Point |
| --- | --- | --- | --- |
| CI/CD (GitHub Actions) | Yes | Controlled release and final cutover | Before card `15` release pipeline hardening and first staging release |
| Build/Release orchestration | Yes | Release governance and cutover workflow | Before card `15` execution and release rehearsal |
| Observability stack | Yes | UAT/prod readiness gate | Before cutover readiness and production go-live checklist |

Current assessment:
- Decision-level blockers for core .NET 10 module migration are closed.
- Remaining blockers are release/readiness phase gates (`15` + UAT/prod).

## Validated Non-Lift-And-Shift Areas

| Legacy Area | Migration Decision | Target Direction |
| --- | --- | --- |
| Legacy auth/session model | Do Not Migrate As-Is | Modern .NET identity/session model via ASP.NET Core Identity + OpenIddict with secure secret handling and password rehash migration path |
| ASMX/WCF/ASHX transport contracts | Do Not Migrate As-Is | Adapter/strangler APIs in modern endpoints, then progressive client cutover |
| Thread-abort agent/service model | Do Not Migrate As-Is | Durable scheduling and worker execution model (Quartz.NET + workers) |
| NAnt + junction release flow | Do Not Migrate As-Is | CI/CD workflows with deterministic builds and artifact promotion |
| FCKeditor/VS2003 legacy artifacts | Do Not Migrate As-Is | Replace editor stack and archive obsolete compatibility artifacts |
| Vendored sample/demo assets | Do Not Migrate As-Is | Exclude from migration scope unless explicitly business-used |

## Guardrails For Free-First Execution

1. Prefer OSS/community editions by default; document any commercial exception before adoption.
2. Add license/compliance review gates for every third-party package before production use.
3. Track CI minutes/storage monthly to avoid accidental cost growth in private repos.
4. Keep paid-upgrade triggers explicit (scale, compliance, support SLA, enterprise features).

## Open Confirmations (Implementation, Not Tool Choice)

1. Confirm legacy user migration approach into ASP.NET Core Identity (rehash-on-login vs staged backfill).
2. Confirm ownership boundaries for `EF Core Migrations` vs `DbUp` scripts by schema/domain.
3. Confirm GitHub Actions environment protections, approvals, and cost guardrails for private-repo usage.
4. Confirm minimum observability SLO gates before production cutover.

## Next Step

After owner confirmation of `Open Confirmations`, copy the approved rows into an `Approved` section and reference them from canonical migration cards/status notes.
