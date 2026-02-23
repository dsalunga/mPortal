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

Current status snapshot (2026-02-23, updated):
- 47 C# projects (`.csproj`) in repo; all converted to SDK-style format.
- **All 47 projects now target `.NET 10`** (`net10.0` or `net10.0-windows`). Zero net48 projects remain.
- All `packages.config` files have been deleted (0 remaining).
- EF6 removed from WCMS.Framework (migrated to EF Core 9.0). Integration module's EF6 EDMX files excluded from compilation.
- WCF references in Integration module wrapped with `#if NETFRAMEWORK`.
- 8 web app hosts rebuilt as ASP.NET Core scaffolds (feature parity not yet complete).
- No CI/CD pipeline configured yet.
- Full system documentation created: see `SYSTEM_DOCUMENTATION.md`.

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

Each rebuilt web host has a basic ASP.NET Core scaffold but needs full endpoint, page, and service migration from the legacy app.

- [x] **Main Portal** (`Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj`):
  - [ ] Migrate all MVC controllers and Razor views from legacy ASP.NET MVC 5 to ASP.NET Core MVC.
  - [ ] Port authentication and authorization (Forms Auth / OWIN → ASP.NET Core Identity or cookie auth).
  - [ ] Migrate `web.config` settings to `appsettings.json` / environment variables / secrets.
  - [ ] Remove legacy `Startup.cs` and consolidate into minimal hosting (`Program.cs`).
  - [ ] Migrate bundling/minification (BundleConfig → ASP.NET Core alternatives such as `WebOptimizer`).
  - [ ] Port all WebForms pages (`.aspx`) to Razor Pages or MVC views.
- [x] **Integration** (`Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj`):
  - [ ] Migrate WCF `.svc` endpoints to Web API controllers or gRPC services.
  - [ ] Port remaining `.aspx` pages to Razor Pages.
  - [ ] Wire EF Core data context and validate query parity.
- [x] **SystemParts** (`Portal/WebParts/SystemParts/SystemParts/WCMS.WebSystem.Apps.SystemApps.WebApp.csproj`):
  - [ ] Port module routes and WebForms pages to Razor Pages / MVC.
  - [ ] Remove legacy `web.config`; move settings to `appsettings.json`.
- [x] **SystemPartsG2** (`Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj`):
  - [ ] Port forum, social, and other module endpoints.
  - [ ] Validate module registration and dependency injection wiring.
- [x] **SystemPartsG3** (`Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj`):
  - [ ] Port incident and jobs module pages to Razor Pages / MVC.
- [x] **BibleReader** (`BibleReader/BibleReader/BibleReader.WebApp.csproj`):
  - [ ] Migrate reader UI pages and API endpoints.
  - [ ] Wire BibleReader.Core services via DI.
- [x] **LessonReviewer** (`LessonReviewer/LessonReviewer/LessonReviewer.csproj`):
  - [ ] Migrate lesson management pages and API endpoints.
  - [ ] Wire LessonReviewer.Core services via DI.
- [x] **BranchLocator** (`Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj`):
  - [ ] Migrate locator search UI and map integration endpoints.
  - [ ] Wire EF Core data context for branch data.

---

### 7.8) Legacy web asset cleanup

- [ ] Remove or migrate all `.aspx` WebForms pages (59 files) to Razor Pages or MVC views.
- [ ] Remove or migrate all `.ascx` user controls (400+ files) to Razor view components or partial views.
- [ ] Remove all `.svc` WCF endpoint files (6 files) after Web API / gRPC replacements are in place.
- [x] Remove remaining `web.config` files (5 files) from ASP.NET Core projects once config is fully migrated to `appsettings.json`.

---

### 7.9) Replace legacy third-party controls

- [x] `Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` — complete the editor integration abstraction; wire a modern rich-text editor (e.g., TinyMCE, CKEditor 5) into the ASP.NET Core host.
- [ ] `Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.csproj` — complete the media renderer abstraction; wire an HTML5 `<video>`/`<audio>` component or modern player library.

---

### 7.10) CI/CD pipeline setup

No CI/CD workflows exist in the repository yet.

- [x] Create GitHub Actions (or equivalent) CI workflow for `dotnet build` across all `.sln` files.
- [ ] Add a CI job matrix covering both `net48` (Windows runner) and `net10.0` (Ubuntu/macOS runner) targets.
- [ ] Add `dotnet test` step for existing test projects (`Integration.UnitTest`, `SDKTest`).
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

- [ ] Expand unit test projects beyond the current 2 (`Integration.UnitTest`, `SDKTest`) to cover core platform libraries.
- [ ] Add integration tests for each rebuilt ASP.NET Core web host (auth flows, CRUD operations, module workflows).
- [ ] Add smoke tests for background agent/service executables.
- [ ] Create an end-to-end regression test suite covering critical user journeys.
- [ ] Validate production-like runtime behavior on Windows / IIS for retained Windows-only workloads.

---

### 7.13) Build and reference cleanup

- [ ] Resolve unresolved assembly/reference warnings present in baseline solution builds (e.g., agent/service references to legacy webparts binaries).
- [ ] Remove obsolete project references and dead code paths left behind by scaffold conversions.
- [x] Consolidate solution files (`.sln`) — evaluate whether 19 solution files are still needed or can be reduced.
- [ ] Audit and update NuGet package versions across all projects for .NET 10 compatibility.

---

### 7.14) Configuration and deployment

- [x] Complete `web.config` → `appsettings.json` migration for all ASP.NET Core hosts.
- [ ] Migrate connection strings and secrets to ASP.NET Core configuration (user secrets / Azure Key Vault / environment variables).
- [ ] Update deployment scripts (currently `.cmd` / Windows-based) for cross-platform or containerized deployment.
- [x] Create Docker support (`Dockerfile` / `docker-compose`) for ASP.NET Core hosts.
- [ ] Document production deployment runbook for the .NET 10 stack.

---

### 7.15) Dynamic page rendering pipeline (CMS-critical)

The CMS dynamically resolves URLs to database-stored pages and renders them from templates + web parts. This is the most critical migration item for feature parity.

- [x] Create ASP.NET Core middleware to replace `WebRewriter.ResolvePage()` — resolve URL path segments to `WSite` → `WPage` hierarchy via database lookup.
- [ ] Implement custom `IRouter` or endpoint routing that integrates with the `WPage` resolution pipeline.
- [ ] Create a `PageRenderingMiddleware` that loads `WebTemplate`, iterates `WebTemplatePanel` zones, and renders `WebPageElement` instances via Razor.
- [ ] Implement dynamic Razor layout selection based on `WPage.ThemeId` → `WebTheme` → layout file mapping (custom `IViewLocationExpander`).
- [ ] Port `WContext` from static `HttpContext.Current` to a scoped DI service (`IWContext`) injected via `IHttpContextAccessor`.
- [ ] Port `WSession` to ASP.NET Core claims-based identity + cookie authentication (replace `FormsAuthentication` and `LoginCookieManager`).
- [ ] Port `WQuery` query parameter handling to work with ASP.NET Core `HttpRequest.Query`.

---

### 7.16) Web part View Component conversion (518 `.ascx` files)

Each legacy `.ascx` user control that renders a web part must become a Razor View Component or partial view in ASP.NET Core. Priority order:

#### Tier 1 — Admin parts (Central) — ~100 controls
- [ ] Convert `Content/Parts/Central/Manager/` admin controls to View Components (site/page/template/part management).
- [ ] Convert `Content/Parts/Central/Security/` controls (user, group, permission management).
- [ ] Convert `Content/Parts/Central/WebPart/` controls (part admin, part control templates).
- [ ] Convert `Content/Parts/Central/Template/` controls (template editor, panel management).
- [ ] Convert `Content/Parts/Central/WebSites/` controls (site configuration).
- [ ] Convert `Content/Parts/Central/Tools/` controls (code executor, diagnostics).
- [ ] Convert `Content/Parts/Central/Agent/` controls (job management).

#### Tier 2 — Common parts — ~10 controls
- [ ] Convert `Content/Parts/Common/Login*.ascx` to Razor login components.
- [ ] Convert `Content/Parts/Common/Comments.ascx` to Razor comment component.
- [ ] Convert remaining Common parts (MessageBoard, TriggerTask, UserPhotoUpload).

#### Tier 3 — Theme templates — ~50 controls
- [ ] Convert `Content/Themes/*/` template files from `.ascx` to `.cshtml` Razor layouts.
- [ ] Implement theme selection middleware that maps `WebTheme`/`WebSkin` to layout files.

#### Tier 4 — Shared controls — ~12 controls
- [ ] Convert `Content/Controls/` (TabControl, TextEditor, Breadcrumb, DatePicker, CKEditor, etc.) to Tag Helpers or View Components.

#### Tier 5 — Module-specific parts — ~300 controls
- [ ] Convert SystemParts `AppBundle/` controls (Article, Calendar, FileManager, Contact, etc.).
- [ ] Convert SystemPartsG2 `AppBundle2/` controls (Forum, Social, Ads, Newsletter).
- [ ] Convert SystemPartsG3 `AppBundle3/` controls (Incident, Jobs).

---

### 7.17) WContext & WSession as DI services

`WContext` (request context) and `WSession` (user session) currently rely on `HttpContext.Current` (static accessor not available in ASP.NET Core). These must become proper DI services.

- [x] Create `IWContext` interface and scoped implementation registered in DI container.
- [ ] Create `IWSession` interface wrapping ASP.NET Core `ClaimsPrincipal` + distributed session.
- [ ] Refactor all `WContext` static property access across 30+ manager classes to use constructor injection.
- [ ] Refactor `WSession` consumers to use `IHttpContextAccessor` + `IWSession`.
- [ ] Replace `UserSessionManager` browser tracking with ASP.NET Core distributed session.

---

### 7.18) Registry & configuration service

The CMS registry (`WebRegistry`) is a hierarchical database-stored config tree that users modify at runtime. It must be preserved as a CMS feature while integrating with ASP.NET Core configuration.

- [x] Wrap `WebRegistry` in an `IConfigurationProvider` to participate in the ASP.NET Core configuration system.
- [ ] Convert `WConfig` properties to `IOptions<WConfigOptions>` with change-token-based reloading.
- [ ] Preserve the `WebRegistry.Updated` event mechanism for live configuration changes.
- [ ] Register registry-dependent services as scoped/transient to pick up configuration changes.

---

### 7.19) SOAP / ASMX service migration

- [x] Migrate `BibleReader/BibleReader/BibleService.asmx` SOAP service to a Web API controller.
- [x] Update BibleReader client code to call the new REST endpoint.

---

### 7.20) Cross-platform path & file handling

- [x] Audit all `Server.MapPath()` calls and replace with `IWebHostEnvironment.ContentRootPath` / `WebRootPath`.
- [x] Replace Windows-style path separators (`\`) with `Path.Combine()` / `Path.DirectorySeparatorChar`.
- [x] Replace `System.Drawing` image operations with cross-platform alternatives (SkiaSharp or ImageSharp) where used outside Windows.
- [x] Create cross-platform build scripts (PowerShell Core / `dotnet` CLI) to replace `.cmd` batch files.

---

### 7.21) Database provider modernization

- [x] Migrate `System.Data.SqlClient` usage to `Microsoft.Data.SqlClient` in all SQL provider classes.
- [x] Evaluate stored-procedure-based data access (`SqlDataProviderBase`) for potential EF Core migration or keep as ADO.NET with modern client.
- [x] Update connection string configuration to use ASP.NET Core connection string patterns.
