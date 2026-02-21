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
| 1 | `Core/WCMS.Common/WCMS.Common.csproj` | Port | Already SDK-style; move `net8.0 -> net10.0`, clean legacy bootstrapper metadata. |
| 1 | `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` | Port | Convert to SDK test project (MSTest/NUnit/xUnit modernization). |
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
| 5 | `Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` | Replace | Replace legacy editor package with supported editor integration. |
| 5 | `Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.vs2003.csproj` | Retire | Obsolete legacy project format. |
| 5 | `Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.csproj` | Replace | Replace legacy ASP.NET control implementation. |
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
2. [ ] Provision Windows VM and verify baseline build of:
   - `Portal/WebSystem/mPortal.sln`
   - `Portal/WebSystem/mPortal-Web.sln`
3. [x] Create migration branch and add shared build settings (`Directory.Build.props`).
   - Completed 2026-02-21: branch `codex/net10-modernization`, added repo-level `Directory.Build.props`.
4. [x] Execute Wave 1 pilot:
   - [x] `Core/WCMS.Common/WCMS.Common.csproj` -> `net10.0`
     - Completed 2026-02-21: upgraded to `net10.0` and removed legacy bootstrapper/ClickOnce metadata.
   - [x] `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` -> SDK-style tests.
     - Completed 2026-02-21: converted to SDK-style MSTest project and validated with `dotnet test` on `net10.0`.
5. [x] Start Wave 2 with `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` and `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj`.
   - Completed 2026-02-21: both projects converted to SDK-style (`net48`) and validated with `dotnet build`.
