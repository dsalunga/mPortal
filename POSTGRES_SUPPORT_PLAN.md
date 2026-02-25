# PostgreSQL Support — Implementation Plan

> **Goal**: Make mPortal CMS fully cross-platform by adding PostgreSQL as an alternative to MS SQL Server.

## Summary of Changes (This PR)

This PR introduces the **core database abstraction layer** that enables PostgreSQL support across the mPortal CMS platform. All data access operations now go through a provider-agnostic interface, allowing the system to run on either SQL Server or PostgreSQL with a single configuration change.

### Architecture Changes

| Layer | Before | After |
|-------|--------|-------|
| ADO.NET Access | `SqlHelper` (static, SQL Server only) | `DbHelper` → `IDbHelper` (factory, multi-provider) |
| Parameters | `SqlParameter` (SQL Server) | `DbParameter` (provider-agnostic) |
| Connections | `SqlConnection` (SQL Server) | `DbConnection` (provider-agnostic) |
| SQL Syntax | `[Column]` (SQL Server brackets) | `DbSyntax.QuoteIdentifier()` (provider-aware) |
| EF Core | `UseSqlServer()` only | `UseSqlServer()` / `UseNpgsql()` via config |
| Health Checks | `AddSqlServer()` only | `AddSqlServer()` / `AddNpgSql()` via config |
| Configuration | Hardcoded SQL Server | `WCMS:DatabaseProvider` setting |

### New Files

- [x] `WCMS.Common/Utilities/Data/DatabaseProvider.cs` — Enum: `SqlServer`, `PostgreSql`
- [x] `WCMS.Common/Utilities/Data/IDbHelper.cs` — Provider-agnostic database interface
- [x] `WCMS.Common/Utilities/Data/DbHelper.cs` — Static factory + convenience methods
- [x] `WCMS.Common/Utilities/Data/DbSyntax.cs` — Cross-database SQL syntax helpers
- [x] `WCMS.Common/Utilities/Data/SqlServerDbHelper.cs` — SQL Server implementation
- [x] `WCMS.Common/Utilities/Data/PostgresDbHelper.cs` — PostgreSQL implementation
- [x] `Tests/WCMS.Framework.Tests/DbHelperTests.cs` — 24 unit tests

### Modified Files

- [x] **43 provider files** migrated from `SqlHelper`/`SqlParameter` to `DbHelper`/`DbParameter`
- [x] `GenericSqlDataProvider.cs` — Uses `DbSyntax.QuoteIdentifier()` for portable SQL
- [x] `GenericSqlDataProviderBase.cs` — Uses `DbHelper` for stored procedure calls
- [x] `VersionSqlDataProvider.cs` — Uses `DbHelper`/`DbSyntax`
- [x] `SqlDataProviderBase.cs` — Uses `DbHelper`
- [x] `ServiceCollectionExtensions.cs` — New `AddWcmsDatabase()` method
- [x] `WConfigOptions.cs` — New `DatabaseProvider` property
- [x] `Program.cs` (WebSystem-MVC) — Provider-aware health checks + DbHelper init
- [x] `appsettings.json` — New `DatabaseProvider` setting
- [x] `docker-compose.yml` — PostgreSQL service with profile support

### NuGet Packages Added

| Package | Version | Project |
|---------|---------|---------|
| `Npgsql` | 9.0.3 | WCMS.Common, Core/WCMS.Common |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 9.0.4 | WCMS.Framework |
| `AspNetCore.HealthChecks.NpgSql` | 9.0.0 | WebSystem-MVC |

---

## Configuration

### Switching to PostgreSQL

**appsettings.json:**
```json
{
  "WCMS": {
    "DatabaseProvider": "PostgreSql"
  },
  "ConnectionStrings": {
    "ConnectionString": "Host=localhost;Port=5432;Database=mPortal;Username=postgres;Password=YourPassword;"
  }
}
```

**Environment variables (Docker):**
```bash
WCMS__DatabaseProvider=PostgreSql
ConnectionStrings__ConnectionString=Host=postgres;Database=mPortal;Username=postgres;Password=YourPassword;
```

**Docker Compose with PostgreSQL:**
```bash
DATABASE_PROVIDER=PostgreSql \
CONNECTION_STRING="Host=postgres;Database=mPortal;Username=postgres;Password=YourPassword;" \
docker compose --profile postgres up
```

### Supported Provider Names

The `DatabaseProvider` setting accepts these values (case-insensitive):
- **SQL Server**: `SqlServer`, `mssql`, `sql` (default if omitted)
- **PostgreSQL**: `PostgreSql`, `postgres`, `npgsql`, `pgsql`

---

## Remaining Work (Future PRs)

The following items are needed for a complete PostgreSQL deployment:

### Phase 2: Schema Migration
- [ ] Create PostgreSQL schema DDL scripts matching the SQL Server schema
- [ ] Create a migration tool or script to convert SQL Server schema → PostgreSQL
- [ ] Handle data type differences (e.g., `nvarchar` → `varchar`/`text`, `datetime` → `timestamp`, `bit` → `boolean`, `uniqueidentifier` → `uuid`)
- [ ] Create PostgreSQL equivalents of stored procedures as functions

### Phase 3: Stored Procedures → PostgreSQL Functions
- [ ] Convert ~40 stored procedures (`WebUser_Get`, `WebPage_Get`, etc.) to PostgreSQL functions
- [ ] Ensure `CommandType.StoredProcedure` works with PostgreSQL function calling convention
- [ ] Alternative: Migrate stored procedures to inline SQL in providers (reduces DB-specific code)

### Phase 4: EF Core DbContext Multi-Provider
- [ ] Update `IntegrationDbContext`, `MusicDbContext`, `ExternalDbContext`, `BranchLocatorDbContext` registration to use config-driven `UseNpgsql()`/`UseSqlServer()`
- [ ] Add EF Core migrations for both providers
- [ ] Test EF Core model compatibility with PostgreSQL data types

### Phase 5: Peripheral Apps
- [ ] Update BibleReader, LessonReviewer, SystemPartsG2/G3 Program.cs files
- [ ] Update DbManager utility for PostgreSQL backup/restore
- [ ] Update Agent/AgentService for PostgreSQL connectivity

### Phase 6: Testing & Validation
- [ ] Integration tests with PostgreSQL (Testcontainers)
- [ ] CI pipeline with both SQL Server and PostgreSQL
- [ ] Performance benchmarking on PostgreSQL
- [ ] Data migration testing (SQL Server → PostgreSQL)

### Phase 7: Docker & Deployment
- [ ] PostgreSQL-specific Dockerfile variant
- [ ] Kubernetes manifests with PostgreSQL support
- [ ] Database initialization scripts (seed data)

---

## Design Decisions & Recommendations

### 1. Why `IDbHelper` instead of full EF Core migration?
The existing codebase uses a **hybrid approach**: reflection-based `GenericSqlDataProvider` + stored procedure-based `GenericSqlDataProviderBase`. A full EF Core migration would require rewriting ~40 providers and their stored procedures. The `IDbHelper` abstraction preserves the existing architecture while enabling PostgreSQL.

### 2. Why keep `SqlHelper.cs`?
`SqlHelper.cs` is preserved for backward compatibility. Legacy code or third-party integrations that directly reference `SqlHelper` will continue to work on SQL Server. New code should use `DbHelper`.

### 3. Stored Procedures Strategy
PostgreSQL supports stored procedures (via `CREATE FUNCTION`), but the calling convention differs. **Recommendation**: Gradually migrate stored procedures to inline parameterized SQL in the providers, which is already the approach used by `GenericSqlDataProvider`. This eliminates the SQL-dialect dependency entirely.

### 4. Identity/Sequence Strategy
SQL Server uses `IDENTITY` columns; PostgreSQL uses `SERIAL`/`SEQUENCE`. The current system uses `WebObject.GetNextRecordId()` for ID generation, which is database-agnostic. This is already compatible with PostgreSQL.

### 5. Docker Compose Profiles
The `docker-compose.yml` uses **profiles** to select the database:
- `docker compose --profile default up` → SQL Server (default)
- `docker compose --profile postgres up` → PostgreSQL
