# 15 - Build Deploy And Local Run Workflow

## Scope
Build scripts, junction setup scripts, and local host/runtime configuration for `/legacy/Portal`.

## Build orchestration
- Root orchestrators: `build-debug.cmd`, `build-release.cmd`.
- Debug flow mostly uses `msbuild` across `WebSystem` and module packs.
- Environment setup uses `_set-env-vars.cmd` (VS 2022/2019 `VsDevCmd.bat` fallback).
- First-time flow (`FIRST-TIME-setup.cmd`) combines junction creation, debug build, DB restore.

## Module packaging and composition
- Module packs are projected into host app via `mklink /j` junction scripts.
- Junction scripts map module `AppBundle`/`Integration`/`BranchLocator` content into:
  `WebSystem-MVC/Content/Parts/*`
- `PostBuildManager` exists for copy/merge logic, but major copy paths are currently disabled in code.

## Local hosting profile
- `Binaries/applicationhost.config` defines IIS Express sites:
- `mPortal`: `http://localhost:9000` (+ `https://localhost:44300`)
- `Lesson Reviewer`: `http://localhost:8889` and `http://localhost:9001` (+ `https://localhost:44301`)

## Reliability findings
- Active release scripts call NAnt build files that are missing (for WebSystem and major module packs).
- Debug scripts are more reliable than release scripts in current repository state.
- Build/deploy assumptions remain Windows-centric (junctions, batch scripts, IIS Express).

## Evaluation
Strengths:
- Practical bootstrap path for full local stack bring-up.
- Clear module-to-host composition pattern through deterministic junctions.

Risks:
- Release pipeline appears stale/non-functional due missing `.build` artifacts.
- Scripted workflow has weak validation and error aggregation across chained steps.

## Key anchors
- `legacy/Portal/build-debug.cmd`
- `legacy/Portal/build-release.cmd`
- `legacy/Portal/_set-env-vars.cmd`
- `legacy/Portal/FIRST-TIME-setup.cmd`
- `legacy/Portal/_create-junction.cmd`
- `legacy/Portal/WebParts/*/*/create-junction.cmd`
- `legacy/Portal/Binaries/applicationhost.config`
- `legacy/Portal/Utilities/PostBuildManager/PostBuildManager/Program.cs`

