# 19 - Testing Quality And Risk Evaluation

## Quality snapshot (analysis date: 2026-04-10)
- Legacy scope sampled: 8,428 files, 48 `.csproj`, 19 `.sln`.
- Service/API surface: 11 `*.asmx`, 6 `*.svc`, 13 `*.ashx`.
- `NotImplementedException` occurrences in `*.cs`: 253.
- `catch (Exception ...)` occurrences in `*.cs`: 143.
- Test attribute occurrences (`[TestMethod]/[Fact]/[Theory]/[Test]`): 2.

## Test posture
- Only one clear unit-test project found for core product modules:
  `WCMS.WebSystem.Apps.Integration.UnitTest`.
- Existing tests are minimal and include weak assertions (for example, `Assert.Equals` usage).
- No broad automated coverage for routing, security, rendering, and data migration-critical paths.

## Top risk clusters
- Security posture:
  legacy password/hash patterns, plain-text config credentials, broad session/static coupling.
- Operational reliability:
  thread abort usage, partial-failure script execution, weak retry/dead-letter patterns.
- Build reproducibility:
  multiple release scripts reference missing NAnt buildfiles.
- Integration fragility:
  external SOAP/service dependencies and mixed protocol endpoints.
- Maintainability:
  high reflection/global-state usage and low test safety net.

## Modernization prioritization (recommended)
1. Stabilize operations and security-critical paths (auth, secrets, job cancellation, restore safety).
2. Repair build reproducibility (release scripts, deterministic module packaging).
3. Add regression tests around request pipeline, auth/session, and high-value integrations.
4. Incrementally extract bounded modules (BranchLocator, LessonReviewer, BibleReader surfaces) from shared global contracts.

## Evaluation
The system is feature-rich and operationally proven, but risk concentration is high in security, release automation, and regression safety. Canonicalization should be followed by targeted hardening and test investments before major refactors.

## Key anchors
- `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/*`
- `legacy/Portal/WebSystem/WCMS.Framework/Utilities/SecurityHelper.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Agent/FrameworkAgent.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Agent/AgentTaskBase.cs`
- `legacy/Portal/build-release.cmd`
- `legacy/LessonReviewer/LessonReviewer/App_Data/Config.xml`

