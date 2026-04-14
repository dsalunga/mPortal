# 04 - Modern Data Providers Caching And Persistence

## Purpose
Set the target persistence and caching architecture for predictable behavior on `.NET 10`.

## Persistence Strategy
- Primary: `EF Core 10` for domain aggregates and transactional workflows.
- Supplement: `DbUp` SQL scripts for complex/stable procedural paths.
- Versioning: enforce ordered schema migrations with rollback-tested deployment bundles.

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
