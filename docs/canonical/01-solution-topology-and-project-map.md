# 01 - Solution Topology And Project Map

## Inventory snapshot
- 48 C# project files in `legacy/`.
- 19 Visual Studio solution files.
- Target frameworks detected:
  - `v4.8`: 38 projects
  - `v4.7`: 6 projects
  - `v4.5.2`: 1 project
  - `v4.0`: 1 project
  - `net8.0`: 1 project (`legacy/Core/WCMS.Common` transitional copy)
  - no explicit framework tag: 1 legacy `vs2003` project

## Topology by area
- `Portal/WebSystem` (12 projects): shared framework/runtime stack and primary web host.
- `Portal/WebParts` (24 projects): module/domain packs and UI bundles.
- `Portal/Utilities` (6 projects): support tools for DB/deploy/extraction/editing.
- `BibleReader` (2 projects): standalone app + core.
- `LessonReviewer` (2 projects): standalone app + core.
- `Core` (1 project): modernized/smaller common library copy.
- `Libraries` (1 project): media player helper control.

## Primary solution entry points
- Main: `legacy/Portal/WebSystem/mPortal.sln`
- Web-focused: `legacy/Portal/WebSystem/mPortal-Web.sln`
- Module packs:
  - `legacy/Portal/WebParts/SystemParts/System-Parts.sln`
  - `legacy/Portal/WebParts/SystemPartsG2/System-Parts-G2.sln`
  - `legacy/Portal/WebParts/SystemPartsG3/System-Parts-G3.sln`
  - `legacy/Portal/WebParts/Integration/Integration.sln`
  - `legacy/Portal/WebParts/BranchLocator/BranchLocator.sln`

## Dependency shape (critical)
- `WCMS.Common` is the base shared utility dependency.
- `WCMS.Framework` depends on `WCMS.Common` and defines domain contracts.
- `WCMS.Framework.Core.SqlProvider` + `...XmlProvider` implement persistence contracts.
- `WCMS.WebSystem.ViewModels` bridges framework objects to UI load patterns.
- `WebSystem-MVC` references framework + providers + agent + module assemblies.

## Evaluation
Architecture is layered in intent, but deployment/runtime coupling is high because the web host references broad module/runtime assemblies directly and expects specific junction/link layouts.
