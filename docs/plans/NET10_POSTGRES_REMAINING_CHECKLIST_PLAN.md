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
  - Code evidence: benchmark harness now exists (`Portal/Utilities/Benchmarks/WCMS.PostgreSql.Benchmarks/`), but legacy-vs-modern comparative run data is still pending

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
      - `Portal/Assets/Database/PostgreSQL/seed-data.sql`
      - `Portal/Assets/Database/PostgreSQL/seed-test-fixtures.sql`
    - Seeded login + CMS render assertions:
      - `Tests/WCMS.Integration.Tests/PostgreSqlProviderIntegrationTests.cs`
        - `AccountLogin_WithSeededFixtureUser_RedirectsWithoutLoginError_AndSetsAuthCookie`
        - `CmsFallback_RootPath_RendersSeededPage`

- [x] `CHK-NET10-009` Close migrated legacy-content runtime gaps for root and BibleReader rendering paths.
  - Current status: implemented
  - Code evidence:
    - Global context/query null-safety and legacy render compatibility hardening:
      - `Portal/WebSystem/WCMS.Framework/WContext.cs`
      - `Portal/WebSystem/WebSystem/_ViewImports.cshtml`
    - Migrated host-native Integration content files for legacy paths:
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/GlobalSwitch/*.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/BibleReader/BibleBrowser.cshtml`
    - Local runtime validation on `http://localhost:8800`:
    - `/` -> `200`
    - `/Central/Security/Login/?RequestUrl=/Central` -> `200`
    - `/BibleReader` -> `200`

- [x] `CHK-NET10-010` Link migrated WebParts modules into the modern web host and close active static legacy-template path gaps.
  - Current status: implemented
  - Code evidence:
    - Web host now references migrated WebParts app modules:
      - `Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj`
        - `..\..\WebParts\SystemParts\SystemParts\WCMS.WebSystem.Apps.SystemApps.WebApp.csproj`
        - `..\..\WebParts\SystemPartsG2\SystemPartsG2\WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj`
        - `..\..\WebParts\SystemPartsG3\SystemPartsG3\WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj`
        - `..\..\WebParts\Integration\IntegrationParts\WCMS.WebSystem.Apps.Integration.WebApp.csproj`
    - Active static-page missing-template sweep now returns zero:
      - `active_static_missing_rows = 0`
      - `active_static_missing_unique = 0`
    - Missing active static template wrappers added for MusicCompetition/Streaming/Social:
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/ASOPMobile.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/MCJudges.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/MCVoteResult.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/MCVoteV2.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/MCVoteResultV3.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/MCJudgeV2.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/MusicCompetition/MCJudgesMaster.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/Integration/Streaming/StreamingConsole.cshtml`
      - `Portal/WebSystem/WebSystem/Content/Parts/AppBundle2/Social/MobileWall.cshtml`
    - Legacy provider discovery hardened for linked module assemblies and object-id binding:
      - `Portal/WebSystem/WCMS.Framework/Core/WebObject.cs` (cross-assembly type resolution for legacy provider/manager names)
      - `Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/WebObjectContent.cs` (resolve manager/provider by `WebObjects.WebObjectContent`)
    - Runtime check after linkage hardening:
      - `Host: localhost` + `/public` -> `200` (no more `WebObjectContent` manager null crash)

- [ ] `CHK-NET10-011` Normalize site-identity routing coverage for host/path variants that still produce `404` despite existing page records.
  - Current status: pending runtime validation/tuning
  - Code evidence:
    - Some host/path combinations now work with the linked modules (for example `Host: dev.bengswi.com` + `/musicportal/Vote` -> `200`).
    - Remaining host/path combinations still need routing identity tuning (for example `Host: localhost` + `/MusicCompetition` -> `404` with existing `WebPage` records).
    - Multi-site host resolution remains the core decision point in `Portal/WebSystem/WCMS.Framework/Middleware/PageResolutionMiddleware.cs`.

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
  - Code evidence: benchmark harness added at `Portal/Utilities/Benchmarks/WCMS.PostgreSql.Benchmarks/` (`BenchmarkDotNet`)
  - Remaining: execute benchmark runs and publish comparative SQL Server vs PostgreSQL results

- [ ] `CHK-PG-007` Execute SQL Server -> PostgreSQL data migration validation (schema + data correctness + rollback plan).
  - Current status: partially implemented
  - Code evidence: `Tests/WCMS.Integration.Tests/DataMigrationValidationTests.cs` added (env-gated dual-DB parity checks)
  - Remaining: run parity checks against provisioned SQL Server and PostgreSQL datasets and capture result evidence

- [x] `CHK-PG-008` Add Kubernetes manifests/config overlays for PostgreSQL deployment (or explicitly mark as out-of-scope).
  - Current status: implemented
  - Code evidence: `Deploy/k8s/` manifests added (`namespace`, PostgreSQL `StatefulSet`, web `Deployment/Service`, config/secret template)

## Remaining Blockers

1. `CHK-PG-006`: execute PostgreSQL benchmark runs and publish comparative results.
2. `CHK-PG-007`: run SQL Server -> PostgreSQL parity checks against provisioned datasets and capture evidence.
3. `CHK-NET10-011`: finalize host/path identity routing for remaining multi-site `404` combinations.
