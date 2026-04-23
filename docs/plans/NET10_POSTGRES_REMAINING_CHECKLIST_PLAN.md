# NET10 + PostgreSQL Remaining Checklist Plan

## Objective

Track and validate the remaining .NET 10 + PostgreSQL migration items using code evidence, with clear separation between implemented work and runtime execution still pending.

## Validation Snapshot (2026-04-23)

- `dotnet restore mPortal.slnx -v minimal` -> success (`All projects are up-to-date for restore`)
- `dotnet build mPortal.slnx --no-restore -v minimal` -> success (`0 Error(s)`, `2 Warning(s)`)
- `dotnet test Tests/WCMS.Framework.Tests/WCMS.Framework.Tests.csproj --no-build -v minimal` -> `77/77` passed
- `dotnet test Tests/WCMS.Integration.Tests/WCMS.Integration.Tests.csproj -v minimal` -> `25/32` passed, `7` skipped (environment-gated PostgreSQL/data-parity suites)

## Checklist

### A) Runtime Validation Items (Implementation mostly in place, environment validation pending)

- [ ] `CHK-NET10-001` Validate production-like runtime behavior on Windows/IIS for retained Windows-only workloads.
  - Current status: pending runtime validation
  - Code evidence: `.github/workflows/build.yml` has `build-windows` job and Windows solution build (`dotnet build mPortal.slnx`)

- [ ] `CHK-NET10-002` Verify background agent service starts and executes scheduled tasks against real database records.
  - Current status: pending runtime validation
  - Code evidence: `Portal/WebSystem/WCMS.Framework.AgentService/Program.cs` registers `FrameworkAgentService`; `FrameworkAgentService` starts `FrameworkAgent`

- [ ] `CHK-NET10-003` Test multi-site hosting (multiple `WSite` entries and host/domain resolution).
  - Current status: pending runtime validation
  - Code evidence: `Portal/WebSystem/WCMS.Framework/Middleware/PageResolutionMiddleware.cs` resolves by `Request.Host.Host` and `WebSiteIdentity.Provider`

- [ ] `CHK-NET10-004` Test admin controls end-to-end (site/page/template/user management) with real admin data.
  - Current status: pending runtime validation
  - Code evidence: `49` admin ViewComponents exist in `Portal/WebSystem/WebSystem/ViewComponents/Admin/`

- [ ] `CHK-NET10-005` Run performance baseline comparison with legacy .NET Framework runtime.
  - Current status: pending benchmark design/execution
  - Code evidence: benchmark harness now exists (`Tools/WCMS.PostgreSql.Benchmarks/`), but legacy-vs-modern comparative run data is still pending

### B) Implementation Gaps Found During Validation (Need code changes, not only runtime checks)

- [x] `CHK-NET10-006` Implement web auth endpoints used by login component forms (`/Account/Login`, `/Account/ForgotPassword`, `/Account/VerifyOtp`, `/logout`) and wire to cookie auth/session flow.
  - Current status: implemented
  - Code evidence:
    - `Portal/WebSystem/WebSystem/Controllers/AccountController.cs` added for form auth flows
    - `Portal/WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs` now handles logout + query-based status messages
    - `Tests/WCMS.Integration.Tests/AuthenticationFlowTests.cs` covers invalid-login redirect and `/logout` behavior

- [x] `CHK-NET10-007` Replace current CMS fallback HTML stub with real page rendering that invokes panel ViewComponents from resolved CMS page context.
  - Current status: implemented
  - Code evidence:
    - `Portal/WebSystem/WebSystem/Controllers/CmsController.cs` added as CMS fallback renderer
    - `Portal/WebSystem/WebSystem/Views/Cms/Render.cshtml` renders dynamic panels via `~/_loader.cshtml`
    - `Portal/WebSystem/WebSystem/Program.cs` now maps catch-all fallback to `CmsController.Render`
    - `Portal/WebSystem/WCMS.Framework/WContext.cs` + `Portal/WebSystem/WebSystem/_loader.cshtml` updated for per-element rendering context propagation

- [x] `CHK-NET10-008` Add integration tests covering login/logout and CMS page rendering with populated database fixtures.
  - Current status: implemented
  - Code evidence:
    - PostgreSQL test harness now auto-applies schema + baseline + fixture scripts:
      - `Tests/WCMS.Integration.Tests/PostgreSqlTestHarness.cs`
      - `Database/PostgreSQL/seed-data.sql`
      - `Database/PostgreSQL/seed-test-fixtures.sql`
    - Seeded login + CMS render assertions:
      - `Tests/WCMS.Integration.Tests/PostgreSqlProviderIntegrationTests.cs`
        - `AccountLogin_WithSeededFixtureUser_RedirectsWithoutLoginError_AndSetsAuthCookie`
        - `CmsFallback_RootPath_RendersSeededPage`

### C) PostgreSQL Work Remaining

- [x] `CHK-PG-001` Close stale plan item for EF package alignment in BranchLocator (already satisfied in code).
  - Current status: complete
  - Code evidence:
    - `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` uses EF Core `10.0.5`
    - `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` uses EF Core `10.0.5`
    - `dotnet restore mPortal.slnx` is clean

- [x] `CHK-PG-002` Add EF Core migrations strategy/artifacts for SQL Server and PostgreSQL where EF contexts are used.
  - Current status: implemented
  - Code evidence:
    - SQL Server migrations retained under `Data/Migrations/SqlServer/**` in module projects
    - BranchLocator PostgreSQL migrations under `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/Data/Migrations/PostgreSql/BranchLocator/`
    - Integration PostgreSQL migrations moved to dedicated assembly `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.Migrations.PostgreSql/` with contexts for `ExternalDbContext`, `IntegrationDbContext`, and `MusicDbContext`
    - Integration runtime wiring now points PostgreSQL contexts to `WCMS.WebSystem.Apps.Integration.Migrations.PostgreSql` via `Program.cs`

- [x] `CHK-PG-003` Validate EF model compatibility with PostgreSQL types using database-backed tests.
  - Current status: implemented
  - Code evidence: `Tests/WCMS.Integration.Tests/EfModelCompatibilityTests.cs` added (`PostgreSql` category)

- [x] `CHK-PG-004` Add PostgreSQL integration tests (Testcontainers or equivalent).
  - Current status: implemented
  - Code evidence: `PostgreSqlTestHarness.cs` + `PostgreSqlProviderIntegrationTests.cs` (`Testcontainers.PostgreSql`)

- [x] `CHK-PG-005` Add CI coverage for both SQL Server and PostgreSQL runtime test lanes.
  - Current status: implemented
  - Code evidence:
    - SQL Server lane: existing `build-windows`
    - PostgreSQL lane: new `integration-postgres` job in `.github/workflows/build.yml`

- [ ] `CHK-PG-006` Execute PostgreSQL performance benchmarking against representative workloads.
  - Current status: partially implemented
  - Code evidence: benchmark harness added at `Tools/WCMS.PostgreSql.Benchmarks/` (`BenchmarkDotNet`)
  - Remaining: execute benchmark runs and publish comparative SQL Server vs PostgreSQL results

- [ ] `CHK-PG-007` Execute SQL Server -> PostgreSQL data migration validation (schema + data correctness + rollback plan).
  - Current status: partially implemented
  - Code evidence: `Tests/WCMS.Integration.Tests/DataMigrationValidationTests.cs` added (env-gated dual-DB parity checks)
  - Remaining: run parity checks against provisioned SQL Server and PostgreSQL datasets and capture result evidence

- [x] `CHK-PG-008` Add Kubernetes manifests/config overlays for PostgreSQL deployment (or explicitly mark as out-of-scope).
  - Current status: implemented
  - Code evidence: `deploy/k8s/` manifests added (`namespace`, PostgreSQL `StatefulSet`, web `Deployment/Service`, config/secret template)

## Remaining Blockers

1. `CHK-PG-006`: execute PostgreSQL benchmark runs and publish comparative results.
2. `CHK-PG-007`: run SQL Server -> PostgreSQL parity checks against provisioned datasets and capture evidence.
