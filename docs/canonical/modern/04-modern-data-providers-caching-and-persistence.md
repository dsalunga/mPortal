# 04 - Modern Data Providers Caching And Persistence

## Purpose
Set the target persistence and caching architecture for predictable behavior on `.NET 10`.

## Persistence Strategy
- Primary: `EF Core 10` for domain aggregates and transactional workflows with `PostgreSQL` as the default provider.
- Supplement: `DbUp` SQL scripts for complex/stable procedural paths.
- Versioning: enforce ordered schema migrations with rollback-tested deployment bundles.

## Ownership Boundaries (EF Core vs DbUp)

| Area | Primary Owner | Notes |
| --- | --- | --- |
| Identity and OpenIddict tables | `EF Core Migrations` | Keep auth schema fully code-first for maintainability. |
| Core CMS metadata tables | `EF Core Migrations` | Use DbUp only for exceptional data-repair/hotfix scripts. |
| Module transactional tables | `EF Core Migrations` | Keep schema evolution with module code ownership. |
| Stored procedures, functions, views, triggers | `DbUp` | SQL-first assets should stay script-owned. |
| Cross-schema transforms and large data migrations | `DbUp` | Safer for ordered, controlled transform scripts. |

Boundary rule: one DB object has one owner tool to prevent migration drift.

## Ownership Registry
- Canonical registry path: `docs/plans/legacy-migration-v2/database/ef-dbup-ownership-registry.md`.
- Machine-readable companion map: `Database/migration-ownership/ownership-map.yaml` (optional, recommended for automation).
- Maintainers: platform/data migration owner (primary), with required review from affected module owner and DB reviewer.

## Caching Strategy
- Use layered caching: in-request cache, distributed cache (Redis-ready), and query-level caching where safe.
- Apply explicit cache keys scoped by site/tenant and invalidation events.
- Avoid static singleton mutable state used in legacy runtime paths.

## Operational Requirements
- Add observability for slow queries, cache hit ratio, and migration execution.
- Define data integrity checks for page/module metadata before cutover.
- Keep backup/restore drills integrated into release runbooks.

## Legacy Reference
- [Legacy card baseline](../legacy/04-data-providers-caching-and-persistence.md)
