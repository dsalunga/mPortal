# EF Core vs DbUp Ownership Registry

This document is the source-of-truth ownership map for schema/database objects in the modern migration.

Rules:
1. One DB object has one primary migration owner (`EF Core Migrations` or `DbUp`).
2. Ownership changes require review from platform/data owner, affected module owner, and DB reviewer.
3. `DbUp`-owned objects must not be altered by EF-generated migrations.

## Ownership Table

| Object Group | Schema | Primary Owner | Secondary Use | Maintainer |
| --- | --- | --- | --- | --- |
| Identity tables (`AspNet*`, OpenIddict) | `auth` | `EF Core Migrations` | None | Platform/Data owner |
| Core CMS metadata tables | `cms` | `EF Core Migrations` | `DbUp` only for exceptional hotfix/data repair scripts | Platform/Data owner + CMS module owner |
| Module transactional tables | per module | `EF Core Migrations` | `DbUp` for one-off data backfills | Platform/Data owner + module owner |
| Procedures/functions/views/triggers | per module/shared | `DbUp` | None | Platform/Data owner + DB reviewer |
| Cross-schema transforms/large backfills | shared | `DbUp` | None | Platform/Data owner + DB reviewer |

## Governance

- Canonical maintainers:
  - Primary: platform/data migration owner.
  - Required reviewers: affected module owner and DB reviewer.
- Optional machine-readable map:
  - `Database/migration-ownership/ownership-map.yaml`
