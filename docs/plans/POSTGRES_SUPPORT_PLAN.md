# PostgreSQL Support — Implementation Plan

> **Goal**: Make mPortal CMS fully cross-platform by adding PostgreSQL as an alternative to MS SQL Server.

## Validation Snapshot (2026-06-26)

- **PostgreSQL local runtime verified**: App runs on PostgreSQL 18.3 (macOS/Homebrew), pages render correctly via `http://localhost:5000/`
- PostgreSQL schema assets are present and versioned:
  - `Database/PostgreSQL/schema.sql` (121 `CREATE TABLE` statements, including 6 columns added for parity: `parentid`, `primaryidentityid`, `protocolid`, `maritalstatusid`, `lastloginfailuredate`, `loginfailurecount`)
  - `Database/PostgreSQL/schema-integration.sql` (16 `CREATE TABLE` statements)
  - `Database/PostgreSQL/schema-biblereader.sql` (60 `CREATE TABLE` statements)
  - `Database/PostgreSQL/seed-data.sql` — Minimal CMS seed data with DataProviderName, TypeName, and ManagerName for all 34 WebObject entities
  - `Database/PostgreSQL/init-db.sh`
- `DbSyntax.QuoteIdentifier()` lowercases identifiers for PostgreSQL (`"columnname"`) to match PostgreSQL's default lowercase convention — fixed in both `Core/WCMS.Common/` and `Portal/WebSystem/WCMS.Common/`
- Provider abstraction and multi-provider wiring in place (`DbHelper`, `DatabaseProvider`, `UseNpgsql()` branches in all 8 host `Program.cs` files)
- `appsettings.Development.json` created for all 8 web hosts with PostgreSQL configuration
- Automated tests currently pass:
  - `Tests/WCMS.Framework.Tests` → 77 passed
  - `Tests/WCMS.Integration.Tests` → 8 passed
  - `Tests/WCMS.WebSystem.Apps.Integration.UnitTest` → 2 passed
  - `Tests/SDKTest` → 1 passed
  - **88 total tests** — all passing
- Remaining implementation gaps:
  - No EF `Migrations/` directories exist yet for dual-provider migration flow
  - GitHub Actions workflows currently have no PostgreSQL service-container lane
  - EF Core package version alignment needed for `WCMS.WebSystem.Apps.BranchLocator`

## Summary of Changes (This PR)

This PR introduces the **core database abstraction layer** and **eliminates all stored procedure dependencies**, enabling PostgreSQL support across the mPortal CMS platform. All data access operations now use inline parameterized SQL through a provider-agnostic interface, allowing the system to run on either SQL Server or PostgreSQL with a single configuration change.

### Architecture Changes

| Layer | Before | After |
|-------|--------|-------|
| ADO.NET Access | `SqlHelper` (static, SQL Server only) | `DbHelper` → `IDbHelper` (factory, multi-provider) |
| Parameters | `SqlParameter` (SQL Server) | `DbParameter` (provider-agnostic) |
| Connections | `SqlConnection` (SQL Server) | `DbConnection` (provider-agnostic) |
| SQL Syntax | `[Column]` (SQL Server brackets) | `DbSyntax.QuoteIdentifier()` (provider-aware) |
| SQL Queries | 115 stored procedures | Inline parameterized SQL (cross-database) |
| Identity | `SCOPE_IDENTITY()` (SQL Server only) | `SCOPE_IDENTITY()` / `RETURNING` (provider-aware) |
| SQL Builder | Manual string building | `SqlBuilder` utility for SELECT/INSERT/UPDATE/DELETE |
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
- [x] `WCMS.Common/Utilities/Data/SqlBuilder.cs` — Cross-database SQL statement builder
- [x] `Tests/WCMS.Framework.Tests/DbHelperTests.cs` — 24 unit tests
- [x] `Tests/WCMS.Framework.Tests/SqlBuilderTests.cs` — 15 unit tests

### Modified Files

- [x] **43 provider files** migrated from `SqlHelper`/`SqlParameter` to `DbHelper`/`DbParameter`
- [x] **38 provider files** migrated from stored procedures to inline parameterized SQL
- [x] **18 peripheral provider files** migrated from `SqlHelper`/`SqlParameter` to `DbHelper`/`DbParameter` with inline SQL
- [x] `GenericSqlDataProviderBase.cs` — New `TableName`/`IdColumn` abstract properties, inline SQL for Get/GetList/Delete/Refresh
- [x] `GenericSqlDataProvider.cs` — Uses `DbSyntax.QuoteIdentifier()` for portable SQL
- [x] `VersionSqlDataProvider.cs` — Uses `DbHelper`/`DbSyntax`
- [x] `SqlDataProviderBase.cs` — Uses `DbHelper`
- [x] `ServiceCollectionExtensions.cs` — New `AddWcmsDatabase()` method
- [x] `WConfigOptions.cs` — New `DatabaseProvider` property
- [x] `Program.cs` (WebSystem) — Provider-aware health checks + DbHelper init
- [x] `Program.cs` (BranchLocator) — Config-driven `UseNpgsql()`/`UseSqlServer()` for EF Core DbContext
- [x] `Program.cs` (IntegrationParts) — Config-driven `UseNpgsql()`/`UseSqlServer()` for 3 EF Core DbContexts
- [x] `appsettings.json` — New `DatabaseProvider` setting
- [x] `docker-compose.yml` — PostgreSQL service with profile support

### Stored Procedures Eliminated

All **115 stored procedures** have been replaced with inline parameterized SQL:

| Operation | Before (SP) | After (Inline SQL) |
|-----------|-------------|---------------------|
| SELECT | `WebUser_Get` | `SELECT * FROM WebUser WHERE ...` |
| INSERT | `WebUser_Set` (id ≤ 0) | `INSERT INTO WebUser (...) VALUES (...); SELECT SCOPE_IDENTITY()` / `RETURNING "Id"` |
| UPDATE | `WebUser_Set` (id > 0) | `UPDATE WebUser SET ... WHERE "Id" = @Id` |
| DELETE | `WebUser_Del` | `DELETE FROM WebUser WHERE "Id" = @Id` |
| COUNT | `WebPage_GetCount` | `SELECT COUNT(1) FROM WebPage WHERE ...` |
| MAX | `WebPage_GetMaxRank` | `SELECT MAX("Rank") FROM WebPage WHERE ...` |

### NuGet Packages Added

| Package | Version | Project |
|---------|---------|---------|
| `Npgsql` | 10.0.2 | WCMS.Common, Core/WCMS.Common |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 10.0.1 | WCMS.Framework |
| `AspNetCore.HealthChecks.NpgSql` | 9.0.0 | WebSystem |

### Tests

- **77 unit tests** (62 existing + 15 SqlBuilder) — all passing
- **8 integration tests** — all passing
- **2 app integration tests** + **1 SDK test** — all passing
- **88 total tests** — all passing

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

### ~~Phase 2: Schema Migration~~ → Phase 2: Schema DDL — **COMPLETE**
- [x] Create PostgreSQL schema DDL scripts matching the SQL Server table structure — **197 tables across 3 databases**
- [x] Handle data type differences (e.g., `nvarchar` → `varchar`/`text`, `datetime` → `timestamp`, `bit` → `boolean`, `uniqueidentifier` → `uuid`)
- [x] Convert `IDENTITY` columns to `SERIAL`/`BIGSERIAL` in PostgreSQL DDL
- [x] `Database/PostgreSQL/schema.sql` — 121 core tables (mPortal CMS)
- [x] `Database/PostgreSQL/schema-integration.sql` — 16 Integration tables
- [x] `Database/PostgreSQL/schema-biblereader.sql` — 60 BibleReader tables
- [x] `Database/PostgreSQL/init-db.sh` — Database initialization script
- ~~Create PostgreSQL equivalents of stored procedures as functions~~ — **DONE: All stored procedures eliminated**

### ~~Phase 3: Stored Procedures → PostgreSQL Functions~~ — **COMPLETE**
- [x] ~~Convert ~40 stored procedures to PostgreSQL functions~~ — **Eliminated all 115 stored procedures; replaced with inline SQL**
- [x] ~~Ensure `CommandType.StoredProcedure` works with PostgreSQL~~ — **No longer needed; all queries use `CommandType.Text`**
- [x] **Migrated all stored procedures to inline parameterized SQL** — reduces DB-specific code entirely

### Phase 4: EF Core DbContext Multi-Provider — **IN PROGRESS**
- [x] Update `IntegrationDbContext`, `MusicDbContext`, `ExternalDbContext`, `BranchLocatorDbContext` registration to use config-driven `UseNpgsql()`/`UseSqlServer()`
- [ ] Align EF Core package versions in `WCMS.WebSystem.Apps.BranchLocator` with `WCMS.Framework` (10.0.x) to remove `NU1605` downgrade restore/build failures.
- [ ] Add EF Core migrations for both providers
- [ ] Test EF Core model compatibility with PostgreSQL data types

### ~~Phase 5: Peripheral Apps~~ — **COMPLETE** (provider migration)
- [x] Migrated all remaining ~55 peripheral provider files from `SqlHelper`/`SqlParameter` to `DbHelper`/`DbParameter` with inline SQL
- [x] SystemParts providers (Menu, Content, Photo, Calendar, FileManager, Article, RemoteIndexer)
- [x] Integration providers (Bible, MusicCompetition, Registration, MemberLink, MemberVisit, Sportsfest, etc.)
- [x] SystemPartsG2/G3 providers (Social, Newsletter, Incident, Jobs)
- [x] BranchLocator provider
- [x] BibleReader providers
- [x] ViewComponents + presenters (Gallery, Search, BibleVerse, GenericList)
- [x] Core/WCMS.Common mirrored files (GenericSqlDataProvider, SqlDataProviderBase)
- [x] VersionSqlDataProvider cleanup — zero `SqlParameter` references remain
- [x] **Zero `SqlHelper` or `new SqlParameter` references** remain outside infrastructure files (`SqlHelper.cs`, `SqlServerDbHelper.cs`)
- [x] **Zero unused `using Microsoft.Data.SqlClient` imports** — removed from 11 framework model files
- [x] Update DbManager utility for PostgreSQL backup/restore — provider-aware schema generation, `GO` batch handling, `DbSyntax.QuoteIdentifier()` for portable SQL
- [x] Agent/AgentService — already uses framework layer (WebJob, AgentTaskBase) which is fully migrated to DbHelper

### Phase 6: Testing & Validation — **PARTIALLY COMPLETE**
- [x] **Local PostgreSQL runtime verified** — App runs on PostgreSQL 18.3, CMS pages render, diagnostic endpoint confirms all providers resolve
- [x] Seed data (`Database/PostgreSQL/seed-data.sql`) with WebObject TypeName, DataProviderName, ManagerName for all entities
- [x] Schema parity — 6 missing columns added to `schema.sql` (`parentid`, `primaryidentityid`, `protocolid`, `maritalstatusid`, `lastloginfailuredate`, `loginfailurecount`)
- [x] `DbSyntax.QuoteIdentifier()` lowercases PostgreSQL identifiers to match convention
- [x] Unit and integration tests updated and passing (88 total)
- [ ] Integration tests with PostgreSQL (Testcontainers) — requires running PostgreSQL instance
- [ ] CI pipeline with both SQL Server and PostgreSQL — requires CI service containers
- [ ] Performance benchmarking on PostgreSQL
- [ ] Data migration testing (SQL Server → PostgreSQL)

### Phase 7: Docker & Deployment
- [x] Docker Compose with PostgreSQL profile (`docker-compose.yml`)
- [x] Database initialization scripts (`Database/PostgreSQL/init-db.sh`)
- [ ] Kubernetes manifests with PostgreSQL support

---

## Design Decisions & Recommendations

### 1. Why `IDbHelper` instead of full EF Core migration?
The existing codebase uses a **hybrid approach**: reflection-based `GenericSqlDataProvider` + inline SQL-based `GenericSqlDataProviderBase`. A full EF Core migration would require rewriting ~40 providers. The `IDbHelper` abstraction preserves the existing architecture while enabling PostgreSQL.

### 2. Why keep `SqlHelper.cs`?
`SqlHelper.cs` is preserved for backward compatibility. Legacy code or third-party integrations that directly reference `SqlHelper` will continue to work on SQL Server. New code should use `DbHelper`.

### 3. Stored Procedures Strategy — COMPLETED
**Decision**: Migrated all 115 stored procedures to inline parameterized SQL. This:
- Eliminates the need to create and maintain PostgreSQL function equivalents
- Reduces database-specific code to zero (all SQL is standard ANSI with `DbSyntax` quoting)
- Uses `SCOPE_IDENTITY()` (SQL Server) / `RETURNING` (PostgreSQL) for identity columns
- Simplifies deployment — no stored procedure scripts to maintain

### 4. Identity/Sequence Strategy
- **INSERT operations**: Use `SELECT SCOPE_IDENTITY()` (SQL Server) or `RETURNING "Id"` (PostgreSQL)
- **`WebObject.GetNextRecordId()`**: Application-level ID generation, already database-agnostic
- **PostgreSQL DDL**: Use `SERIAL` or `BIGSERIAL` for auto-increment columns

### 5. SqlBuilder Utility
New `SqlBuilder` class provides a fluent API for building cross-database SQL:
```csharp
var (sql, parms) = SqlBuilder.For("WebSkin")
    .Where("ObjectId", "ObjectId", objectId)
    .Where("RecordId", "RecordId", recordId)
    .BuildSelect();  // → SELECT * FROM WebSkin WHERE [ObjectId] = @ObjectId AND [RecordId] = @RecordId
```

### 6. Docker Compose Profiles
The `docker-compose.yml` uses **profiles** to select the database:
- `docker compose --profile default up` → SQL Server (default)
- `docker compose --profile postgres up` → PostgreSQL
