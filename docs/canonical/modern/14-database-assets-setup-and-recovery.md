# 14 - Modern Database Assets Setup And Recovery

## Purpose
Define modern database lifecycle management and recovery standards for migration and production.

## Schema And Migration Model
- Use migration-based versioning (`EF Core Migrations + DbUp`) as baseline.
- Keep SQL project artifacts only where necessary and clearly bounded.
- Require forward-only deployment scripts with tested rollback playbooks.

## Recovery And Backups
- Standardize backup cadence, retention, and restore validation drills.
- Automate recovery verification in non-prod environments.
- Document RTO/RPO targets per critical data domain.

## Release Integration
- Database migrations are first-class release artifacts in CI/CD.
- Gate deployments on migration dry-run/validation checks.
- Record migration history and ownership in release logs.

## Legacy Reference
- [Legacy card baseline](../legacy/14-database-assets-setup-and-recovery.md)
