# .NET 10 Modernization Plan for mPortal

Date: 2026-02-21

## 1) Current-state snapshot

- 48 C# projects (`.csproj`) in repo.
- 1 SDK-style project already on modern .NET:
  - `Core/WCMS.Common/WCMS.Common.csproj` (`net8.0`)
- 47 legacy .NET Framework projects (mostly `v4.8`, with some `v4.7`, `v4.5.2`, `v4.0`).
- 9 legacy web application projects (ASP.NET Framework web project GUID present).
- 34 projects reference `System.Web*`.
- 23 projects still use `packages.config`.
- WebForms/WCF footprint is significant (many `.aspx/.ascx/.master` and `.svc` assets).

Conclusion: this is an incremental migration (strangler pattern), not an in-place framework retarget.

---

## 2) Target architecture decisions

- Runtime target: `.NET 10` (LTS).
- Web target: ASP.NET Core (minimal hosting / MVC / Razor Pages, by module).
- Migration style:
  - Keep existing .NET Framework app running.
  - Introduce new ASP.NET Core host(s) and migrate modules progressively.
  - Move shared libraries first, then module libraries, then web surfaces.
- For shared libraries, use staged conversion:
  1. legacy csproj -> SDK-style while staying on `net48`
  2. add second target (`net10.0`) where feasible
  3. remove `net48` only after consuming web app is off System.Web.

---

## 3) Waves and delivery gates

## Wave 0 - Environment + Baseline (1-2 weeks)

Goals:
- Freeze baseline behavior and deployment flows.
- Capture dependency graph and runtime behaviors.
- Stand up dual-environment dev flow (macOS + Windows VM).

Exit gate:
- Baseline smoke test checklist documented.
- Build/test scripts runnable for both old and new tracks.

## Wave 1 - Tooling and Pilot Conversion (1-2 weeks)

Goals:
- Establish modernization conventions in-repo:
  - `Directory.Build.props`
  - optional centralized package management
  - CI matrix skeleton for `net48` + `net10.0`
- Move the existing modern project from `net8.0` to `net10.0`.
- Pilot one non-web test project to SDK-style + modern test runner.

Exit gate:
- At least one non-web project and one test project build on .NET 10.

## Wave 2 - Core Platform Libraries (4-6 weeks)

Goals:
- Convert platform libraries used by most apps to SDK-style.
- Introduce dual-targeting where possible.
- Remove direct `packages.config` usage in these libraries.

Exit gate:
- Core dependency chain compiles in SDK-style.
- `net10.0` builds pass for migrated subset.

## Wave 3 - Feature Module Libraries (4-8 weeks)

Goals:
- Migrate module libraries (SystemParts/Integration/BranchLocator/BibleReader/LessonReviewer cores).
- Replace or isolate APIs that bind to `System.Web`.

Exit gate:
- Feature libraries consumed by new ASP.NET Core host compile/run.

## Wave 4 - Satellite Web Apps (6-10 weeks)

Goals:
- Rebuild smaller web apps/modules in ASP.NET Core first.
- Keep legacy endpoints running while each module is cut over.

Exit gate:
- Satellite app parity for auth, routing, config, and core workflows.

## Wave 5 - Main WebSystem Host Cutover (8-14 weeks)

Goals:
- Migrate `Portal/WebSystem/WebSystem-MVC` surface to ASP.NET Core.
- Replace legacy dependencies (OWIN, old ASP.NET MVC 5 package stack, WebForms controls).
- Complete config transition (`web.config` -> `appsettings.*`, secrets, env vars).

Exit gate:
- Main portal production cutover to ASP.NET Core / .NET 10.

## Wave 6 - Utilities + DB Projects + Legacy Cleanup (2-4 weeks)

Goals:
- Port/replace Windows utilities (`net10.0` or `net10.0-windows`).
- Decide strategy for SQL projects (`.sqlproj`) in Windows lane.
- Remove obsolete projects/scripts and simplify build/release.

Exit gate:
- Legacy-only build paths removed or archived.

---

## 4) Project-by-project wave map

Legend:
- `Port`: convert to SDK-style and migrate to modern target(s).
- `Rebuild`: recreate web app/module in ASP.NET Core.
- `Replace`: swap old library/control with supported alternative.
- `Retire`: remove after equivalent functionality exists.
- `Hold`: keep temporarily on .NET Framework until dependencies move.

| Wave | Project | Action | Notes |
|---|---|---|---|
| 1 | [x] `Core/WCMS.Common/WCMS.Common.csproj` | Port | Upgraded to `net10.0` and legacy bootstrapper/ClickOnce metadata removed (completed 2026-02-21). |
| 1 | [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` | Port | Converted to SDK-style MSTest project and validated on `net10.0` (completed 2026-02-21). |
| 2 | [x] `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` | Port | Root shared lib; high leverage, System.Web adapters likely needed. |
| 2 | [x] `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` | Port | Depends on WCMS.Common; EF6 presence to isolate. |
| 2 | [x] `Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` | Port | Shared data layer dependency. |
| 2 | [x] `Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj` | Port | Shared provider layer. |
| 2 | [x] `Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` | Port | SQL SMO package modernization required. |
| 2 | [x] `Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` | Port | Depends on framework/provider chain. |
| 2 | [x] `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` | Port | Heavily coupled to ASP.NET MVC APIs; adapter/abstraction pass needed. |
| 2 | [x] `Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` | Port | Background/agent executable path. |
| 2 | [x] `Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` | Port | Service executable; rehost as worker service in .NET 10. |
| 2 | [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` | Port | Cross-solution core dependency for main host. |
| 3 | [x] `BibleReader/BibleReader.Core/BibleReader.Core.csproj` | Port | Shared core for BibleReader and Integration web app. |
| 3 | [x] `LessonReviewer/LessonReviewer.Core/LessonReviewer.Core.csproj` | Port | Shared core for LessonReviewer web app. |
| 3 | [x] `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` | Port | EF6-backed module library. |
| 3 | [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` | Port | Integration support library. |
| 3 | [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` | Port | EF6 + WCF touchpoints; high complexity. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` | Port | Legacy package references; remove `packages.config`. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` | Port | Module aggregation library. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/WCMS.WebSystem.Apps.Contact.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` | Port | Module viewmodel library migration. |
| 3 | [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/WCMS.WebSystem.Apps.Forum.csproj` | Port | Low-risk module library candidate. |
| 3 | [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` | Port | Module aggregation library migration. |
| 3 | [x] `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` | Port | Module library migration. |
| 3 | [x] `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj` | Port | Module library migration. |
| 4 | [x] `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj` | Rebuild | Replaced legacy host with ASP.NET Core `net10.0` scaffold wired to BranchLocator module for incremental endpoint/page migration. |
| 4 | [x] `BibleReader/BibleReader/BibleReader.WebApp.csproj` | Rebuild | Replaced legacy WebForms host with ASP.NET Core `net10.0` scaffold to stage incremental BibleReader feature migration. |
| 4 | [x] `LessonReviewer/LessonReviewer/LessonReviewer.csproj` | Rebuild | Replaced legacy WebForms host with ASP.NET Core `net10.0` scaffold for phased LessonReviewer feature migration. |
| 4 | [x] `Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj` | Rebuild | Replaced legacy mixed host with ASP.NET Core `net10.0` scaffold to stage service and page endpoint cutover incrementally. |
| 4 | [x] `Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj` | Rebuild | Replaced legacy SystemPartsG2 host with ASP.NET Core `net10.0` scaffold for phased module endpoint migration. |
| 4 | [x] `Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj` | Rebuild | Replaced legacy SystemPartsG3 host with ASP.NET Core `net10.0` scaffold and staged module-link migration. |
| 4 | [x] `Portal/WebParts/SystemParts/SystemParts/WCMS.WebSystem.Apps.SystemApps.WebApp.csproj` | Rebuild | Replaced legacy SystemParts host with ASP.NET Core `net10.0` scaffold to drive phased module route/page migration. |
| 4 | [x] `Portal/WebParts/SDKTest/SDKTest/SDKTest.csproj` | Retire | Legacy web project retired and replaced with SDK-style `net10.0` automated smoke-test harness for CI. |
| 5 | [x] `Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj` | Rebuild | Legacy primary host replaced with ASP.NET Core `net10.0` scaffold baseline to drive phased portal cutover. |
| 5 | [x] `Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` | Replace | Replaced legacy control project with SDK-style `net10.0` editor integration abstraction for modern host consumption. |
| 5 | [x] `Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.vs2003.csproj` | Retire | Obsolete VS2003 project removed and replaced with retirement marker documentation. |
| 5 | [x] `Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.csproj` | Replace | Replaced legacy ASP.NET control project with SDK-style `net10.0` media-renderer abstraction. |
| 6 | [x] `Portal/Utilities/DbManager/DbManager/DbManager.csproj` | Port | Converted to SDK-style (`net48`) as interim CLI; move to `net10.0` after core runtime deps are off `System.Web`. |
| 6 | [x] `Portal/Utilities/PostBuildManager/PostBuildManager/PostBuildManager.csproj` | Port | Converted to SDK-style (`net48`) as interim step; cross-platform `net10.0` move requires removing `System.Web` dependency. |
| 6 | [x] `Portal/Utilities/WebExtractor/WebExtractor/WebExtractor.csproj` | Port | Converted to SDK-style (`net48`) as interim step; target `net10.0` after `WCMS.Common` dependencies are modernized. |
| 6 | [x] `Portal/Utilities/WebSystemDeployer/WebSystemDeployer/WebSystemDeployer.csproj` | Port | Converted to SDK-style (`net48`) in Windows lane; final `net10.0-windows`/CLI replacement remains after dependency cleanup. |
| 6 | [x] `Portal/Utilities/MySQL TableEditor/TableEditor.csproj` | Port | Converted to SDK-style (`net48`) as interim Windows utility; move to `net10.0-windows` after legacy dependency cleanup. |
| 6 | [x] `Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` | Port | Converted to SDK-style `net10.0-windows` with WPF + Windows targeting enabled for cross-OS restore/build workflows. |

### SQL projects (Windows lane, parallel to Waves 2-6)

| Wave | Project | Action | Notes |
|---|---|---|---|
| 2-6 (parallel) | `Portal/WebSystem/WCMS.Framework.SqlDabase/WCMS.Framework.SqlDabase.sqlproj` | Hold/Port | Keep in Windows pipeline first; evaluate SSDT/DacFx strategy. |
| 2-6 (parallel) | `Portal/WebParts/Integration/BibleReader.Database/BibleReader.Database.sqlproj` | Hold/Port | Same as above. |
| 2-6 (parallel) | `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.Database/WCMS.WebSystem.Apps.Integration.Database.sqlproj` | Hold/Port | Same as above. |

---

## 5) macOS prerequisites for this migration

Required on this Mac:

- .NET SDK 10.x installed and validated (`dotnet --info`).
- VS Code + C# Dev Kit, or Rider.
- Docker Desktop (for app containers and optional SQL workflows).
- `upgrade-assistant` global tool (optional helper despite deprecation):
  - `dotnet tool install -g upgrade-assistant`

Strongly recommended:

- A Windows 11 VM (Parallels/Dev Box/Azure VM) with:
  - Visual Studio 2022 (current patch)
  - SSDT / Data storage workload for `.sqlproj`
  - IIS + IIS Express
  - .NET Framework 4.8.1 runtime/dev pack

Why Windows VM is still needed:

- Legacy ASP.NET Framework runtime + IIS workflows are Windows-specific.
- Existing repo scripts are `.cmd` and Visual Studio dev-command based.
- SQL Server project tooling (`.sqlproj`) is Windows-centric in this repo.

---

## 6) Immediate next actions (first 2 weeks)

1. [x] Install prerequisites on macOS and confirm `dotnet --info` shows SDK 10.x.
   - Completed 2026-02-21: .NET SDK `10.0.103` and global `upgrade-assistant` installed.
2. [x] Verify baseline build of:
   - `Portal/WebSystem/mPortal.sln`
   - `Portal/WebSystem/mPortal-Web.sln`
   - Completed 2026-02-22 on macOS: both solutions build successfully with `dotnet build`.
   - Windows VM remains recommended for IIS-specific runtime and SQL project (`.sqlproj`) workflows.
3. [x] Create migration branch and add shared build settings (`Directory.Build.props`).
   - Completed 2026-02-21: branch `codex/net10-modernization`, added repo-level `Directory.Build.props`.
4. [x] Execute Wave 1 pilot:
   - [x] `Core/WCMS.Common/WCMS.Common.csproj` -> `net10.0`
     - Completed 2026-02-21: upgraded to `net10.0` and removed legacy bootstrapper/ClickOnce metadata.
   - [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` -> SDK-style tests.
     - Completed 2026-02-21: converted to SDK-style MSTest project and validated with `dotnet test` on `net10.0`.
5. [x] Start Wave 2 with `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` and `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj`.
   - Completed 2026-02-21: both projects converted to SDK-style (`net48`) and validated with `dotnet build`.

---

## 7) Outstanding Work (Not Yet Fully Implemented)

Current status snapshot (2026-02-25, updated):
- 48 C# projects (`.csproj`) on disk; all 48 now included in `mPortal.slnx` (including `Tests/WCMS.Framework.Tests`). All converted to SDK-style format.
- **All 48 projects now target `.NET 10`** — 45 on `net10.0`, 3 on `net10.0-windows` (DbManagerWPF, WebSystemDeployer, MySQL TableEditor). Zero net48 projects remain.
- All `packages.config` files have been deleted (0 remaining).
- EF6 removed from WCMS.Framework (migrated to EF Core 9.0). All EDMX `.edmx` files and EF6 code-behind files deleted.
- WCF `System.ServiceModel` fully removed from codebase. All `#if NETFRAMEWORK` guards removed (0 remaining).
- 8 web app hosts rebuilt as ASP.NET Core scaffolds with DI wiring, endpoint routing, and configuration.
- All legacy `.aspx`, `.ascx`, `.svc`, `.asmx`, `.ashx`, `Global.asax`, `Startup.cs`, `Web.config`, and `.cmd` script files deleted. Zero legacy web assets remain.
- All 19 legacy `.sln` files deleted; `mPortal.slnx` is the single solution file (48 projects).
- Legacy `App_Start/` directory and `Service References/` directory in WebSystem-MVC deleted. Integration `Service References/` still exists (to be cleaned up).
- 270 ViewComponents created (replacing legacy `.ascx` user controls); 271 `Default.cshtml` view files exist. All view refinements complete for portal core, SystemParts, and SystemPartsG2 modules.
- `IWContext` and `IWSession` DI interfaces created and registered via `AddWcmsFramework()`.
- `PageResolutionMiddleware` and `PageRenderingMiddleware` replace legacy URL rewriting and page rendering.
- `CmsPageEndpointRouteBuilderExtensions.MapCmsPages()` provides endpoint routing integration for CMS pages.
- `WConfigOptions` with `IOptions<T>` pattern created; `UserSessionManager` enhanced with `IDistributedCache` support.
- `docker-compose.yml` created for multi-container development with SQL Server.
- CI build workflow configured (`.github/workflows/build.yml`); deployment pipeline not yet configured.
- Full system documentation created: see `SYSTEM_DOCUMENTATION.md`.
- BibleReader.Core, LessonReviewer.Core, and BranchLocator services wired via DI.

Important:
- `[x]` rows in the wave map indicate the planned migration task was executed (including scaffold/interim conversions).
- They do **not** imply full feature parity, production readiness, or complete .NET 10 retargeting for every dependency chain.

---

### 7.1) Remove `packages.config` and migrate to `PackageReference` (23 projects)

All projects are SDK-style but 23 still carry legacy `packages.config` files. These must be converted to `<PackageReference>` entries inside the csproj.

- [x] `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` — migrate packages.config to PackageReference.
- [x] `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` — migrate packages.config to PackageReference.
- [x] `Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` — migrate packages.config to PackageReference.
- [x] `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` — migrate packages.config to PackageReference.
- [x] `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` — migrate packages.config to PackageReference.
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` — migrate packages.config to PackageReference.
- [x] `Portal/Utilities/MySQL TableEditor/TableEditor.csproj` — migrate packages.config to PackageReference.
- [x] Migrate remaining `packages.config` files across all other affected projects (module and utility libraries).
- [x] Validate that `dotnet restore` / `dotnet build` still succeeds after each conversion.

---

### 7.2) Remove or abstract `System.Web` dependencies (29 projects)

`System.Web` is not available on .NET 10. Each reference must be removed, replaced with an ASP.NET Core equivalent, or hidden behind an abstraction layer.

#### Core platform libraries (high leverage — unblocks everything else)

- [x] `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` — replace `System.Web`, `System.Web.Extensions`, `System.Data.OracleClient`, and ASP.NET Razor/WebPages 3.2.3 references; introduce abstractions for `HttpContext`-dependent helpers.
- [x] `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` — remove `System.Web` reference; isolate EF6 usage behind a data-access abstraction; remove ASP.NET Razor/WebPages references.
- [x] `Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` — remove `System.Web` reference.
- [x] `Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` — remove `System.Web` reference.
- [x] `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` — remove `System.Web`, `System.Web.Extensions`, `System.Web.Services` references; replace ASP.NET MVC 5 (`Microsoft.AspNet.Mvc 5.2.3`) with ASP.NET Core MVC equivalents.
- [x] `Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` — remove `System.Web.Extensions` reference.

#### Module libraries

- [x] `Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` — remove `System.Web` and ASP.NET Razor/WebPages 3.2.3 references.
- [x] `Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` — remove `System.Web`, `System.Web.Services`, and `System.ServiceModel` (WCF) references; replace with ASP.NET Core equivalents (see also §7.4 WCF).
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` — remove `System.Web` reference.
- [x] `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` — remove `System.Web` and ASP.NET Razor/WebPages references.
- [x] `BibleReader/BibleReader.Core/BibleReader.Core.csproj` — remove `System.Web` reference.
- [x] `LessonReviewer/LessonReviewer.Core/LessonReviewer.Core.csproj` — remove `System.Web` reference.

#### Utilities

- [x] `Portal/Utilities/PostBuildManager/PostBuildManager/PostBuildManager.csproj` — remove `System.Web` reference.
- [x] `Portal/Utilities/DbManager/DbManager/DbManager.csproj` — remove `System.Web.Extensions` reference.

---

### 7.3) Migrate Entity Framework 6 to EF Core (3 projects)

EF6 does not support .NET 10. These projects must migrate data access code to EF Core (or an alternative).

- [x] `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` — replace `EntityFramework 6.1.3` with `Microsoft.EntityFrameworkCore` and update `DbContext`/mapping/query code.
- [x] `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` — replace `EntityFramework 6.1.3` with EF Core; update `DbContext` and migration files.
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` — replace `EntityFramework 6.1.3` with EF Core; update `DbContext`, mappings, and query patterns.

---

### 7.4) Replace WCF services (1 project, 6 `.svc` endpoints)

WCF server-side hosting is not supported on .NET 10. Each `.svc` endpoint must be replaced.

- [x] Inventory all `.svc` service contracts and operations in the Integration module.
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` — remove `System.ServiceModel` reference; rewrite service endpoints as ASP.NET Core Web API controllers or gRPC services.
- [x] Update all client-side callers to use new HTTP/gRPC contracts.

---

### 7.5) Replace ASP.NET MVC 5 references (1 project)

- [x] `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` — remove `Microsoft.AspNet.Mvc 5.2.3`, `Microsoft.AspNet.Razor`, and `Microsoft.AspNet.WebPages` packages; replace view-model helpers and HTML-helper references with ASP.NET Core MVC equivalents.

---

### 7.6) Retarget remaining `net48` projects to `net10.0` (33 projects)

After System.Web, EF6, WCF, and MVC 5 blockers are resolved, retarget each library to `net10.0` (or `net10.0-windows` for desktop/Windows-only utilities).

#### Core platform libraries

- [x] `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` — retarget `net48` → `net10.0`; verify SQL SMO NuGet packages are available for .NET 10.
- [x] `Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` — retarget `net48` → `net10.0`.

#### Agent / service executables

- [x] `Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` — retarget `net48` → `net10.0`; convert to console app or worker service.
- [x] `Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` — retarget `net48` → `net10.0`; rehost as a .NET `BackgroundService` / worker service.

#### Module libraries — SystemParts

- [x] `Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/WCMS.WebSystem.Apps.Contact.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` — retarget `net48` → `net10.0`.

#### Module libraries — SystemPartsG2

- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/WCMS.WebSystem.Apps.Forum.csproj` — retarget `net48` → `net10.0`.

#### Module libraries — SystemPartsG3

- [x] `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj` — retarget `net48` → `net10.0`.

#### Module libraries — Integration / BibleReader / BranchLocator / LessonReviewer

- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` — retarget `net48` → `net10.0`.
- [x] `BibleReader/BibleReader.Core/BibleReader.Core.csproj` — retarget `net48` → `net10.0`.
- [x] `LessonReviewer/LessonReviewer.Core/LessonReviewer.Core.csproj` — retarget `net48` → `net10.0`.

#### Utilities (Windows lane)

- [x] `Portal/Utilities/DbManager/DbManager/DbManager.csproj` — retarget `net48` → `net10.0` (or `net10.0-windows` if WinForms/WPF used).
- [x] `Portal/Utilities/PostBuildManager/PostBuildManager/PostBuildManager.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/Utilities/WebExtractor/WebExtractor/WebExtractor.csproj` — retarget `net48` → `net10.0`.
- [x] `Portal/Utilities/WebSystemDeployer/WebSystemDeployer/WebSystemDeployer.csproj` — retarget `net48` → `net10.0-windows` (deployment scripts may be Windows-only).
- [x] `Portal/Utilities/MySQL TableEditor/TableEditor.csproj` — retarget `net48` → `net10.0-windows`.

---

### 7.7) Complete feature-parity for rebuilt ASP.NET Core web hosts (8 scaffold apps)

Each rebuilt web host has a basic ASP.NET Core scaffold but needs full endpoint, page, and service migration from the legacy app. All hosts use minimal hosting (`Program.cs`) with `appsettings.json` config.

- [x] **Main Portal** (`Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj`):
  - [x] Scaffold ASP.NET Core minimal hosting with `appsettings.json`, cookie auth, DI registration (`AddWcmsFramework()`), and `PageResolutionMiddleware`.
  - [x] Create API controllers (FrameworkApi, AccountApi, DataSyncApi, UserApi) replacing legacy WCF/ASMX.
  - [x] Create 68 ViewComponents (49 Admin + 19 Theme/Core).
  - [ ] Migrate all MVC controllers and Razor views from legacy ASP.NET MVC 5 to ASP.NET Core MVC.
  - [x] Port authentication and authorization (Forms Auth / OWIN → ASP.NET Core Identity or cookie auth) — cookie auth configured; `FormsAuthentication` removed from `LoginSecurity.cs`.
  - [ ] Migrate bundling/minification — replace with ASP.NET Core alternatives such as `WebOptimizer`.
  - [x] Remove legacy `Startup.cs` (OWIN-based) — deleted.
  - [x] Clean up legacy `App_Start/` directory and `Service References/` directory — deleted.
- [x] **Integration** (`Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj`):
  - [x] Create `MemberApiController` replacing WCF `.svc` endpoints.
  - [x] Create 130 ViewComponents for Integration module.
  - [x] Wire API controller endpoints to actual data layer — all controllers verified as wired to real data providers.
  - [ ] Wire EF Core data context and validate query parity.
- [x] **SystemParts** (`Portal/WebParts/SystemParts/SystemParts/WCMS.WebSystem.Apps.SystemApps.WebApp.csproj`):
  - [x] Create `ContentApiController` and 34 ViewComponents.
  - [ ] Port module routes and remaining pages to Razor Pages / MVC.
- [x] **SystemPartsG2** (`Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj`):
  - [x] Create 21 ViewComponents for forum, social, and other modules.
  - [ ] Port forum, social, and other module endpoints.
  - [ ] Validate module registration and dependency injection wiring.
- [x] **SystemPartsG3** (`Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj`):
  - [x] Create 10 ViewComponents for incident and jobs modules.
  - [ ] Port incident and jobs module pages to Razor Pages / MVC.
- [x] **BibleReader** (`BibleReader/BibleReader/BibleReader.WebApp.csproj`):
  - [x] Create `BibleApiController` replacing ASMX SOAP service.
  - [x] Create `BibleVerseViewComponent`.
  - [ ] Migrate reader UI pages.
  - [x] Wire BibleReader.Core services via DI — BibleManager, BibleVersionProvider, BibleBookNameProvider, BibleVersionLanguageProvider, GenericBibleVerseProvider registered.
- [x] **LessonReviewer** (`LessonReviewer/LessonReviewer/LessonReviewer.csproj`):
  - [ ] Migrate lesson management pages and API endpoints.
  - [x] Wire LessonReviewer.Core services via DI — MakeUpServiceSession registered as scoped.
  - [x] Create ViewComponents for lesson management UI — `LessonListViewComponent`, `LessonPlayerViewComponent`, and `LessonScheduleViewComponent` created.
- [x] **BranchLocator** (`Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj`):
  - [ ] Migrate locator search UI and map integration endpoints.
  - [x] Create ViewComponents for locator UI — `BranchLocatorViewComponent` and `BranchMapViewComponent` created.
  - [x] Wire EF Core data context for branch data — BranchLocatorDbContext and IMChapterProvider registered.

---

### 7.8) Legacy web asset cleanup

- [x] Remove all `.aspx` WebForms pages — all deleted (0 remaining).
- [x] Remove all `.ascx` user controls — all deleted (0 remaining); replaced by 269 ViewComponents.
- [x] Remove all `.svc` WCF endpoint files — all deleted (0 remaining); replaced by API controllers.
- [x] Remove all `.asmx` SOAP endpoint files — all deleted (0 remaining).
- [x] Remove remaining `web.config` files from ASP.NET Core projects — all 8 `Web.config` files deleted.
- [x] Remove `.ashx` HTTP handler files (13) and code-behinds (10) — all deleted.
- [x] Remove `Global.asax` files and code-behinds (3 pairs) — all deleted.
- [x] Remove legacy `Startup.cs` (OWIN-based) from `WebSystem-MVC` — deleted.

---

### 7.9) Replace legacy third-party controls

- [x] `Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` — complete the editor integration abstraction; wire a modern rich-text editor (e.g., TinyMCE, CKEditor 5) into the ASP.NET Core host.
- [x] `Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.csproj` — HTML5 `<video>` renderer created (`MediaPlayerRenderer.RenderVideoTag`) replacing legacy ASP.NET media control.

---

### 7.10) CI/CD pipeline setup

- [x] Create GitHub Actions CI workflow for `dotnet build` (`.github/workflows/build.yml` — runs on push/PR to `master`, `codex/net10-modernization`, and `feat/update-net10-migration-tasks`; builds core libraries, web apps, and all 7 web hosts on .NET 10).
- [x] Add `dotnet test` step for existing test projects (`WCMS.Framework.Tests`, `SDKTest`) — tests run in CI with blocking failures (removed `|| true` fallback).
- [x] Add `Tests/WCMS.Framework.Tests` project to `mPortal.slnx` — added; now 48 projects in solution.
- [ ] Add a CI job matrix covering both `net48` (Windows runner) and `net10.0` (Ubuntu/macOS runner) targets.
- [ ] Configure deployment pipeline(s) for staging / production environments.
- [ ] Wire SQL project (`.sqlproj`) build into the Windows CI lane using SSDT or `Microsoft.Build.Sql`.

---

### 7.11) SQL project strategy (3 `.sqlproj` files)

- [x] Evaluate migration of `.sqlproj` files to `Microsoft.Build.Sql` SDK-style projects for cross-platform builds.
- [x] `Portal/WebSystem/WCMS.Framework.SqlDabase/WCMS.Framework.SqlDabase.sqlproj` — migrate or document Windows-only build strategy.
- [x] `Portal/WebParts/Integration/BibleReader.Database/BibleReader.Database.sqlproj` — same as above.
- [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.Database/WCMS.WebSystem.Apps.Integration.Database.sqlproj` — same as above.
- [ ] Integrate DacFx-based deployment into CI/CD pipeline.

---

### 7.12) Testing and regression coverage

- [x] Create `WCMS.Framework.Tests` project with unit tests for core infrastructure (ConfigUtil, WQuery, DI registration) — 17 tests passing.
- [ ] Add integration tests for each rebuilt ASP.NET Core web host (auth flows, CRUD operations, module workflows).
- [ ] Add smoke tests for background agent/service executables.
- [ ] Create an end-to-end regression test suite covering critical user journeys.
- [ ] Validate production-like runtime behavior on Windows / IIS for retained Windows-only workloads.

---

### 7.13) Build and reference cleanup

- [x] Resolve unresolved assembly/reference warnings — no dead project references found; build produces 0 errors, 0 warnings.
- [x] Remove obsolete project references and dead code paths — verified; all project references resolve correctly.
- [x] Consolidate solution files (`.sln`) — `mPortal.slnx` created with 47 projects; 19 legacy `.sln` files deleted (see §8.4). Note: `Tests/WCMS.Framework.Tests` is not yet included (see §7.10).
- [x] Audit and update NuGet package versions — `SystemWebAdapters` (1.3.0→2.0.0), `System.Configuration.ConfigurationManager` (8.0.0→9.0.0), `System.Drawing.Common` (8.0.8→9.0.0) updated in `Core/WCMS.Common`; `Microsoft.NET.Test.Sdk` updated to 18.0.1; all EF Core packages consistent at 9.0.0.

---

### 7.14) Configuration and deployment

- [x] Complete `web.config` → `appsettings.json` migration for all ASP.NET Core hosts.
- [x] Migrate connection strings to ASP.NET Core configuration — connection strings defined in `appsettings.json` for WebSystem-MVC, Integration (IntegrationDb, MusicDb, ExternalDb), and BranchLocator (BranchLocatorDb). Production secrets should use user secrets / Azure Key Vault / environment variables.
- [ ] Update deployment scripts (currently `.cmd` / Windows-based) for cross-platform or containerized deployment.
- [x] Create Docker support — `Dockerfile` created (multi-stage build for `WCMS.WebSystem.WebApp.dll` on .NET 10, port 8080). `docker-compose.yml` not yet created.
- [ ] Document production deployment runbook for the .NET 10 stack.

---

### 7.15) Dynamic page rendering pipeline (CMS-critical)

The CMS dynamically resolves URLs to database-stored pages and renders them from templates + web parts. This is the most critical migration item for feature parity.

- [x] Create ASP.NET Core middleware to replace `WebRewriter.ResolvePage()` — resolve URL path segments to `WSite` → `WPage` hierarchy via database lookup (`PageResolutionMiddleware`).
- [x] Implement custom `IRouter` or endpoint routing that integrates with the `WPage` resolution pipeline — `CmsPageEndpointRouteBuilderExtensions.MapCmsPages()` created as a fallback endpoint that renders pages resolved by `PageResolutionMiddleware`.
- [x] Create a `PageRenderingMiddleware` that loads page template, iterates panel zones, and maps `WebPageElement` instances to ViewComponents — stores panel-to-element mappings in `HttpContext.Items` for Razor consumption. Registered via `app.UseWcmsPageRendering()`.
- [x] Implement dynamic Razor layout selection based on `WPage.ThemeId` → `WebTheme` → layout file mapping — `ThemeViewLocationExpander` created; registered via `services.AddWcmsThemeSupport()`.
- [x] Port `WContext` from static `HttpContext.Current` to a scoped DI service (`IWContext`) injected via `IHttpContextAccessor` — `IWContext` interface created and registered as scoped service via `AddWcmsFramework()`; legacy `WContext.GetInstance()` guarded with `#if NETFRAMEWORK`.
- [x] Port `WSession` to ASP.NET Core cookie authentication — `IWSession` interface created and registered in DI; cookie auth configured in Program.cs; `FormsAuthentication` usage removed from `LoginSecurity.cs`.
- [x] Port `WQuery` query parameter handling to work with ASP.NET Core `HttpRequest.Query` — ported with `#if NETFRAMEWORK` guards for legacy WebForms overloads; 9 unit tests passing.

---

### 7.16) Web part View Component conversion

All legacy `.ascx` user controls have been deleted. 269 ViewComponents have been created across all modules. 271 Razor views (`.cshtml`) exist. Razor views use Bootstrap 5 placeholder markup and need refinement to match original UI (see §8.2).

**Infrastructure (completed):**
- [x] Create `WViewComponent` base class in WCMS.Framework (replaces `WUserControl`/`UserControl` with DI-injected `IWContext`).

#### Tier 1 — Admin parts — 49 ViewComponents created
- [x] Convert admin controls to ViewComponents (site/page/template/part/user/group/permission management, tools, agent) — 49 admin ViewComponents created in `WebSystem-MVC/ViewComponents/Admin/`.

#### Tier 2 — Common parts — completed
- [x] Convert `Login.ascx` → `LoginViewComponent` (authentication form with login/logout/OTP/forgot-password views).
- [x] Convert `Breadcrumb.ascx` → `BreadcrumbViewComponent` (navigation trail).
- [x] Convert `SideBar.ascx` → `SideBarViewComponent` (sidebar container).
- [x] Convert `Comments.ascx` → `CommentsViewComponent` (comment list + post form).
- [x] Convert remaining Common parts: `MessageBoardViewComponent`, `TriggerTaskViewComponent`, `UserPhotoUploadViewComponent`.

#### Tier 3 — Theme templates — 11 ViewComponents created
- [x] Convert theme template controls to ViewComponents — 11 theme ViewComponents created (`ThemeBasic`, `ThemeDefault`, `ThemeCentralResponsive`, `ThemeBootstrap3Navbar`, etc.).
- [x] Implement theme selection middleware that maps `WebTheme`/`WebSkin` to layout files — `ThemeViewLocationExpander` created (see §7.15).

#### Tier 4 — Shared controls — completed
- [x] Convert `CascadeMenu.ascx` → `NavigationViewComponent` (hierarchical menu with Bootstrap nav).
- [x] Convert shared controls to Tag Helpers — created `<wcms-tabs>`/`<wcms-tab>` (Bootstrap 5 nav-tabs), `<wcms-editor>` (CKEditor 5 CDN integration), `<wcms-datepicker>` (HTML5 date input) in `WCMS.Framework/TagHelpers/`.

#### Tier 5 — Module-specific parts (completed)
- [x] Convert SystemParts module controls — 34 ViewComponents created (Content, Article, Contact, Search, Gallery, Feedback, Calendar, FileManager, Survey, etc.).
- [x] Convert SystemPartsG2 module controls — 21 ViewComponents created (Forum, Social, Ads, Newsletter, Downloads, Wall, etc.).
- [x] Convert SystemPartsG3 module controls — 10 ViewComponents created (Incident, Jobs).
- [x] Convert Integration module controls — 130 ViewComponents created (Member management, MusicCompetition, Registration, Profile, Streaming, etc.).
- [x] Convert `BibleVerseView.ascx` → `BibleVerseViewComponent` (Bible verse reader) — 1 ViewComponent in BibleReader.
- [x] Create BranchLocator ViewComponents — `BranchLocatorViewComponent` and `BranchMapViewComponent` created.
- [x] Create LessonReviewer ViewComponents — `LessonListViewComponent`, `LessonPlayerViewComponent`, and `LessonScheduleViewComponent` created.

---

### 7.17) WContext & WSession as DI services

`WContext` (request context) and `WSession` (user session) currently rely on `HttpContext.Current` (static accessor not available in ASP.NET Core). These must become proper DI services.

- [x] Create `IWContext` interface and scoped implementation registered in DI container.
- [x] Create `IWSession` interface and scoped implementation registered in DI container via `AddWcmsFramework()`.
- [x] Refactor `WContext` static property access — `WContext.GetInstance()` is guarded with `#if NETFRAMEWORK`; new code uses `IWContext` via DI.
- [x] Bridge `WSession.Current` to DI — `WSession.Configure(IHttpContextAccessor)` enables static accessor to resolve `IWSession` from `RequestServices` first, falling back to legacy `System.Web` session for backwards compatibility. `UseWcmsFramework()` wires this at startup in all 8 web hosts.
- [x] Replace `UserSessionManager` in-memory `MemoryCache<UserSession>` browser tracking with ASP.NET Core distributed session/cache — `UserSessionManager` enhanced with optional `IDistributedCache` constructor parameter; write-through pattern with in-process `MemoryCache` fallback.

---

### 7.18) Registry & configuration service

The CMS registry (`WebRegistry`) is a hierarchical database-stored config tree that users modify at runtime. It must be preserved as a CMS feature while integrating with ASP.NET Core configuration.

- [x] Wrap `WebRegistry` in an `IConfigurationProvider` — created `WebRegistryConfigurationProvider` and `WebRegistryConfigurationSource` in `WCMS.Framework/Extensions/`; use `builder.Configuration.AddWebRegistry()` to enable.
- [x] Convert `WConfig` properties to `IOptions<WConfigOptions>` with change-token-based reloading — `WConfigOptions` class created in `WCMS.Framework/Configuration/`; `AddWcmsConfiguration()` extension method binds to `"WConfig"` configuration section.
- [x] Preserve the `WebRegistry.Updated` event mechanism for live configuration changes — `WebRegistryConfigurationProvider.Reload()` method available for event-driven refresh.
- [ ] Register registry-dependent services as scoped/transient to pick up configuration changes.

---

### 7.19) SOAP / ASMX service migration

- [x] Migrate `BibleReader/BibleReader/BibleService.asmx` SOAP service to a Web API controller.
- [x] Update BibleReader client code to call the new REST endpoint.

---

### 7.20) Cross-platform path & file handling

- [x] Audit all `Server.MapPath()` calls and replace with cross-platform `PathMapper.MapPath()` — created `PathMapper` utility in WCMS.Common; configured via `PathMapper.Configure()` in all Program.cs files. Most `Server.MapPath` calls replaced; 3 files retain `Server.MapPath` usage via `SystemWebAdapters` shim (`WebHelper.cs` ×2, `MemberHelper.cs` ×1) and 2 are in comments (`LoginSecurity.cs`). These work at runtime through the `Microsoft.AspNetCore.SystemWebAdapters` package but should be migrated to `PathMapper` for consistency.
- [x] Replace Windows-style path separators (`\`) with `Path.Combine()` / `Path.DirectorySeparatorChar`.
- [x] Replace `System.Drawing` image operations with cross-platform alternative — `System.Drawing.Common` NuGet package (9.0.0) already referenced for cross-platform support; Windows desktop apps use native System.Drawing; `FileManagerBase.cs` excluded from compilation. Heavy image processing in `EventRegisterUtil.cs`, `ImageUtil.cs`, `QRCodeUtil.cs`, and `ImageSecurity.cs` uses `System.Drawing.Common` which works cross-platform via NuGet. Future optimization: consider SkiaSharp/ImageSharp for improved performance.
- [x] Create cross-platform build scripts (PowerShell Core / `dotnet` CLI) to replace `.cmd` batch files — `dotnet build`/`dotnet test` via CI; 43 legacy `.cmd` scripts remain on disk but are not required for the .NET 10 build.

---

### 7.21) Database provider modernization

- [x] Migrate `System.Data.SqlClient` usage to `Microsoft.Data.SqlClient` in all SQL provider classes.
- [x] Evaluate stored-procedure-based data access (`SqlDataProviderBase`) for potential EF Core migration or keep as ADO.NET with modern client.
- [x] Update connection string configuration to use ASP.NET Core connection string patterns.

---

## 8) Phase 2 — Integration Testing & Production Readiness

The following items require additional work beyond the initial migration (§7). These ensure the migrated application is fully functional end-to-end.

---

### 8.1) Integration module — EF6 EDMX & WCF data layer replacement

The Integration module's EF6 EDMX models and WCF service methods are wrapped with `#if NETFRAMEWORK` (inactive on .NET 10). The new API controllers (MemberApiController, etc.) need to be wired to the actual data layer.

**EDMX → EF Core code-first migration (Integration database):**
- [x] Reverse-engineer the Integration EDMX model into EF Core `DbContext` + entity classes — EDMX files deleted; entity classes remain in the project.
- [x] Create `IntegrationDbContext` with `OnModelCreating` configuration — created in `Data/IntegrationDbContext.cs` with 9 entity mappings (MemberLink, MemberVisit, GenericRegistration, MCCandidate, MCInterpreterScore, MCSongScore, MCVote, MCComposer, Sportsfest).
- [x] Map entity classes to DbSet properties — all available [ObjectColumn]-annotated entities mapped.
- [x] Register `IntegrationDbContext` in DI via `AddDbContext<IntegrationDbContext>()` in Integration web host — registered along with `MusicDbContext` and `ExternalDbContext`.
- [ ] Update all Integration SQL providers to use EF Core or `Microsoft.Data.SqlClient` ADO.NET instead of EF6

**EDMX → EF Core code-first migration (BranchLocator database):**
- [x] Reverse-engineer the BranchLocator EDMX model into EF Core — EDMX file deleted; `MChapter` entity class exists.
- [x] Create `BranchLocatorDbContext` with entity mappings — created in `Data/BranchLocatorDbContext.cs` mapping `MChapter`.
- [x] Register in DI — `AddDbContext<BranchLocatorDbContext>()` wired in BranchLocator web host.
- [ ] Update `MChapterSqlProvider` to use EF Core

**WCF service method replacement:**
- [x] Wire `MemberApiController` endpoints to actual `MemberSqlProvider` / `MemberManager` data calls — verified; all endpoints use real `MemberLink.Provider` data layer.
- [x] Wire `DataSyncApiController` to actual `WebObjectManager` / `WebSiteManager` export/import logic — verified; uses `WebUser.GetList()`, `WebSiteIdentity.Provider`, `WebUser.Get()`.
- [x] Wire `UserApiController` to actual `WebUserManager` / `WebUserGroupManager` / `WebUserRoleManager` — verified; uses `AccountHelper.ValidateLogin()`, `WSession.UserSessions`, `WebUser.Get()`.
- [x] Wire `ContentApiController` to actual `WebPageManager` / `WebPartManager` / `WebPageElementManager` — verified; uses `WebContent.Get()`, `WebContent.Provider`.
- [x] Wire `AccountApiController` to actual `WebUser.Login()` / `Registration` logic — verified; uses `AccountHelper.ValidateLogin()`, `_wSession.Login()`.
- [x] Wire `FrameworkApiController` to actual `WebSiteManager` / `WebRegistryManager` / `WebTemplateManager` — verified; uses `AccountHelper.ValidateLogin()`, `WebComment` CRUD operations.
- [x] Remove `#if NETFRAMEWORK` guards from Integration WCF methods — all 12 files cleaned up; zero NETFRAMEWORK guards remain.
- [x] Delete legacy `.svc` files — all deleted (see §7.8).

---

### 8.2) ViewComponent Razor view refinement

The 269 ViewComponents have functional C# classes wired to the CMS framework. Their Razor views (`.cshtml`) have been created with Bootstrap 5 markup. Tier 1 components have been refined to production-quality with ARIA accessibility, responsive design, and Bootstrap 5 patterns.

**Approach:** For each ViewComponent, enhance the `.cshtml` to production-quality markup matching the original UI.

**Portal core components (19 — highest priority):**
- [x] `LoginViewComponent` — responsive card layout, input groups, client-side validation, autocomplete attributes, ARIA labels
- [x] `BreadcrumbViewComponent` — breadcrumb separator and link styling
- [x] `NavigationViewComponent` — Bootstrap 5 navbar with multi-level dropdown, active state marking, ARIA roles
- [x] `SideBarViewComponent` — sidebar panel layout
- [x] Theme components (11) — Bootstrap 5 header/footer/layout controls with ARIA markup
- [x] `CommentsViewComponent`, `MessageBoardViewComponent`, `TriggerTaskViewComponent`, `UserPhotoUploadViewComponent` — enhanced to production markup with Bootstrap 5, ARIA labels, semantic HTML, empty-state handling.

**SystemParts components (34):**
- [x] All 34 SystemParts components enhanced to production-quality Bootstrap 5 markup with ARIA labels, responsive design, empty-state handling, and semantic HTML.

**SystemPartsG2 components (21):**
- [x] All 21 components enhanced to production-quality Bootstrap 5 markup with ARIA labels, responsive design, empty-state handling, and semantic HTML.

**SystemPartsG3 components (10):**
- [ ] All 10 Incident/Jobs components — enhance to production markup

**Admin components (49):**
- [ ] All 49 admin components — enhance to production markup (lower priority, iteratively improved)

**Integration components (130):**
- [ ] Account/Registration components — enhance to production markup
- [ ] Profile/LessonReviewer components — enhance to production markup
- [ ] MasterList/EventRegister components — enhance to production markup
- [ ] MusicCompetition components — enhance to production markup
- [ ] Streaming/BibleReader/Reminder/Theme components — enhance to production markup

**BibleReader component (1):**
- [ ] `BibleVerseViewComponent` — enhance to production markup

**LessonReviewer components (3):**
- [ ] `LessonListViewComponent`, `LessonPlayerViewComponent`, `LessonScheduleViewComponent` — enhance to production markup

**BranchLocator components (2):**
- [ ] `BranchLocatorViewComponent`, `BranchMapViewComponent` — enhance to production markup

---

### 8.3) End-to-end testing with database

- [ ] Set up a test SQL Server database with the WCMS schema
- [ ] Run the main web host (`WebSystem-MVC`) and verify page rendering pipeline works end-to-end
- [ ] Verify all 7 API controllers return correct data
- [ ] Verify cookie authentication login/logout flow
- [ ] Verify background agent service starts and executes scheduled tasks
- [ ] Verify ViewComponents render correctly when invoked from pages
- [ ] Test multi-site hosting (multiple WSite entries resolving different domains)
- [ ] Test admin controls (site/page/template/user management)
- [ ] Performance baseline comparison with legacy .NET Framework version

---

### 8.4) Legacy file cleanup

- [x] Delete all legacy `.aspx` files — completed (0 remaining).
- [x] Delete all legacy `.ascx` files — completed (0 remaining).
- [x] Delete all legacy `.svc` files — completed (0 remaining).
- [x] Delete all legacy `.asmx` files — completed (0 remaining).
- [x] Delete legacy `Global.asax` files and code-behinds (3 pairs) — deleted.
- [x] Delete legacy `.ashx` HTTP handler files (13) and code-behinds (10) — deleted.
- [x] Delete legacy `Startup.cs` (OWIN-based) from WebSystem-MVC — deleted.
- [x] Delete EDMX files (4: WFrameworkModel.edmx, MusicModel.edmx, ExternalDBModel.edmx, WeeklySchedulerModel.edmx) — deleted. EF6 code-behind files also deleted.
- [x] Remove `<Compile Remove>` entries from `.csproj` files — cleaned up `EnableDefaultCompileItems`/`EnableDefaultContentItems` overrides in 7 web SDK projects; deleted all files referenced by `<Compile Remove>` in WebSystem-MVC, Integration, WCMS.Common (Portal), WCMS.Framework, and ViewModels projects; removed corresponding `<Compile Remove>` entries.
- [x] Remove `EnableDefaultContentItems` / `EnableDefaultCompileItems` overrides — removed from all 7 web SDK projects (LessonReviewer, BibleReader, BranchLocator, SystemParts, SystemPartsG2, SystemPartsG3, Integration); SDK auto-discovery now handles .cs and .cshtml files.
- [x] Clean up remaining `<Compile Remove>` entries — files referenced by `<Compile Remove>` have been deleted; 27 entries remain across csproj files for glob patterns and legitimate exclusions (Properties/AssemblyInfo.cs, Controls/**, Apps/**, etc.).
- [x] Consolidate or remove legacy `.sln` files — all 19 legacy `.sln` files deleted; `mPortal.slnx` remains as the single solution file (48 projects including `WCMS.Framework.Tests`).

---

### 8.5) Additional migration items

The following items were identified during review and are not fully covered by other sections:

**Authentication migration:**
- [x] Remove `FormsAuthentication` usage from `LoginSecurity.cs` — removed (was already commented out; dead comment deleted).
- [x] Bridge `WSession.Current` to DI — `WSession.Current` now resolves `IWSession` from `RequestServices` via `IHttpContextAccessor` when available, falling back to legacy path for non-DI scenarios (see §7.17).

**HTTP handler migration (.ashx → middleware/minimal API):**
- [x] All 13 `.ashx` handler files and 10 code-behind files deleted (business logic to be re-implemented in API controllers/middleware as needed).

**Server.MapPath migration:**
- [x] Replace `Server.MapPath()` calls — mostly completed via `PathMapper` utility (see §7.20). 3 active usages remain via `SystemWebAdapters` shim (`WebHelper.cs` ×2, `MemberHelper.cs` ×1).

**System.Drawing migration:**
- [x] `System.Drawing.Common` NuGet package (9.0.0) referenced for cross-platform support; used in 10+ files (`ImageUtil.cs`, `ImageSecurity.cs`, `QRCodeUtil.cs`, `EventRegisterUtil.cs`, `FileManagerBase.cs`, Windows desktop apps). Future optimization: consider SkiaSharp/ImageSharp for improved performance.

**ViewComponent view gaps:**
- [x] All 269 ViewComponents have corresponding `Default.cshtml` view files — verified; 271 view files exist (no gaps remain).

**Legacy EF6 code-behind files:**
- [x] Delete 3 EF6 auto-generated code-behind files — `MusicModel.Context.cs`, `ExternalDBModel.Designer.cs`, `WeeklySchedulerModel.Designer.cs` all deleted.

**Legacy WCF reference:**
- [x] Remove `System.ServiceModel` — fully removed from codebase; all `#if NETFRAMEWORK` guards removed from 12 files.

**Legacy `Compile Remove` cleanup:**
- [x] Clean up `<Compile Remove>` entries — deleted all files referenced by explicit `<Compile Remove>` entries in WCMS.Common (Portal), WCMS.Framework, WCMS.WebSystem.ViewModels, and Integration projects; removed corresponding entries from csproj files.

**Legacy files on disk (excluded from compilation):**
- [x] Delete legacy `App_Start/` directory — deleted from WebSystem-MVC.
- [x] Delete `Service References/` directory from WebSystem-MVC — deleted.
- [x] Delete `Content/Controllers/CatController.cs` from WebSystem-MVC — deleted.
- [x] Delete legacy `.cmd` scripts (43 files) — all deleted.

**`WSession.Current` static accessor:**
- [x] `WSession.Current` bridges to DI automatically — `WSession.Configure(IHttpContextAccessor)` resolves `IWSession` from `RequestServices` first; all 8 web hosts call `UseWcmsFramework()`. 19 files still use `WSession.Current` but the static bridge means they work correctly with DI.

**`docker-compose.yml`:**
- [x] Create `docker-compose.yml` — created with SQL Server 2022 + web app services, health checks, and volume persistence.

**.NET 10 GA validation:**
- [ ] Validate entire solution builds and runs on the .NET 10 GA release (currently on preview/RC SDK `10.0.103`).
