# Canonical Cards Migration Status

This document maps canonical cards (`docs/canonical/00-19`) to migration status and decision guidance so implementation stays focused on core/essential features.

Status legend:
- `Core - Migrate`: required in target architecture.
- `Core - Migrate With Redesign`: required outcome, but not a direct lift-and-shift.
- `Selective - Validate First`: migrate only validated/high-usage subsets.
- `Retain Legacy Temporarily`: keep in legacy during phased cutover.
- `Do Not Migrate As-Is`: replace, retire, or reimplement differently.

## Card-Level Decision Table

| Canonical Card | Criticality | Migration Status | Recommended Action | Validation Focus |
|---|---|---|---|---|
| `00-legacy-system-overview` | High | Core - Migrate | Use as baseline scope guard and dependency map | Confirm all high-value user journeys are represented |
| `01-solution-topology-and-project-map` | High | Core - Migrate | Use as decomposition blueprint for target services/modules | Validate ownership boundaries per module |
| `02-runtime-request-pipeline-and-rendering` | High | Core - Migrate With Redesign | Rebuild request/render pipeline in modern stack | Validate URL behavior, routing parity, rendering outputs |
| `03-core-domain-model-and-webobject-registry` | High | Core - Migrate With Redesign | Preserve domain semantics; redesign metadata registry implementation | Validate object identity, ownership, and permission semantics |
| `04-data-providers-caching-and-persistence` | High | Core - Migrate With Redesign | Replace provider/reflection patterns with explicit data/service layers | Validate query parity, cache invalidation, data integrity |
| `05-security-authentication-and-session-control` | Critical | Core - Migrate With Redesign | Rebuild security/auth/session using modern identity + secrets handling | Validate auth flows, RBAC, password lifecycle, auditability |
| `06-central-admin-console-capabilities` | High | Selective - Validate First | Migrate admin features by business usage priority | Validate editor/admin workflows that are truly required |
| `07-webpart-platform-and-loading-contracts` | High | Core - Migrate With Redesign | Preserve extensibility contract, replace file/junction loading model | Validate part lifecycle, config, and rendering extensibility |
| `08-systemparts-g1-modules` | High | Selective - Validate First | Migrate high-value modules first (article/calendar/file manager/etc.) | Validate each module owner, usage, and dependencies |
| `09-systemparts-g2-modules` | Medium | Selective - Validate First | Migrate only active social/forum/newsletter capabilities | Validate live usage and retirement candidates |
| `10-systemparts-g3-modules` | Medium | Selective - Validate First | Migrate incident/jobs if still operationally required | Validate incident/job process ownership |
| `11-integration-suite-and-external-services` | Critical | Core - Migrate With Redesign | Rebuild integration contracts (API-first) vs direct legacy transport reuse | Validate external contract parity, retries, observability |
| `12-branch-locator-module` | Medium | Core - Migrate | Good early extraction candidate with bounded blast radius | Validate geo/search/API response compatibility |
| `13-background-agent-and-job-execution` | High | Core - Migrate With Redesign | Replace thread-abort scheduler with durable job orchestration | Validate schedule correctness, retries, idempotency |
| `14-database-assets-setup-and-recovery` | Critical | Core - Migrate With Redesign | Move from script-batch restore to migration-managed schema/data strategy | Validate rollback/recovery and drift controls |
| `15-build-deploy-and-local-run-workflow` | High | Core - Migrate With Redesign | Replace NAnt/cmd/junction flow with reproducible CI/CD pipelines | Validate deterministic builds and release promotion |
| `16-standalone-app-biblereader` | Medium | Selective - Validate First | Migrate if still strategic; otherwise retain/retire by usage data | Validate active users and API dependencies |
| `17-standalone-app-lessonreviewer` | Medium | Selective - Validate First | Migrate if still needed; prioritize security hardening first | Validate access model, media workflows, and demand |
| `18-utilities-and-tooling` | Medium | Do Not Migrate As-Is | Keep only needed operational functions; reimplement as internal tooling/jobs | Validate which utilities are still actively used |
| `19-testing-quality-and-risk-evaluation` | Critical | Core - Migrate | Use as release gate policy and tracking baseline | Validate test coverage milestones before cutover |

## Do Not Migrate As-Is (Validated)

| Area | Decision | Why |
|---|---|---|
| Vendored sample/demo assets (e.g., legacy plugin docs/samples) | Do Not Migrate As-Is | Not business functionality; replace via package-managed dependencies |
| `FredCK.FCKeditorV2.vs2003.csproj` | Do Not Migrate As-Is | Obsolete legacy artifact; keep only for historical reference if needed |
| NAnt + junction-specific release orchestration | Do Not Migrate As-Is | Environment-bound and non-reproducible; replace with modern CI/CD |
| Utility GUIs with unclear ownership/use (example: legacy table editors) | Do Not Migrate As-Is | High maintenance risk, unclear product value |

## Cannot Directly Migrate (Needs Redesign)

| Area | Constraint | Target Direction |
|---|---|---|
| Legacy membership/session/auth patterns | Old framework assumptions and weak secret handling | Modern identity provider + token/session standards |
| ASMX/WCF mixed integration endpoints | Transport/runtime mismatch with modern service posture | Contract-first HTTP APIs with compatibility adapters |
| Reflection-heavy provider wiring and global state | Implicit behavior is hard to reason about/test | Explicit dependency injection + typed service boundaries |
| Thread-based agent execution with abort semantics | Unsafe cancellation and limited reliability guarantees | Durable job runners with retries, backoff, and observability |

## Minimum Core Feature Set To Protect

1. Security + identity + authorization semantics (`05`).
2. Core domain object integrity and metadata behavior (`03`, `04`).
3. Request routing/rendering behavior for active user journeys (`02`, `07`).
4. Business-critical module workflows from G1/G2/G3/Integration (`08`-`11`).
5. Reliable data migration/recovery and release pipeline (`14`, `15`).

