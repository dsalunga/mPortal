# Legacy Canonical Cards

This folder contains focused architecture and feature cards for the `/legacy` codebase.

Analysis snapshot:
- Date: 2026-04-10
- Scope: `legacy/`
- Size sampled: 8,428 files, 48 `.csproj`, 19 `.sln`
- Service surface sampled: 11 `*.asmx`, 6 `*.svc`, 13 `*.ashx`
- DB assets sampled: 137 `WebObject` registrations, 139 table scripts, 333 procedure scripts

How to use this set:
1. Start with system/runtime cards (`00` to `07`).
2. Review module pack cards (`08` to `12`) for functional domains.
3. Use operations cards (`13` to `18`) for deployment, data, jobs, and utilities.
4. Use `19` for quality/risk evaluation and modernization prioritization.

Card index:
- [00 - Legacy System Overview](./00-legacy-system-overview.md)
- [01 - Solution Topology And Project Map](./01-solution-topology-and-project-map.md)
- [02 - Runtime Request Pipeline And Rendering](./02-runtime-request-pipeline-and-rendering.md)
- [03 - Core Domain Model And WebObject Registry](./03-core-domain-model-and-webobject-registry.md)
- [04 - Data Providers Caching And Persistence](./04-data-providers-caching-and-persistence.md)
- [05 - Security Authentication And Session Control](./05-security-authentication-and-session-control.md)
- [06 - Central Admin Console Capabilities](./06-central-admin-console-capabilities.md)
- [07 - WebPart Platform And Loading Contracts](./07-webpart-platform-and-loading-contracts.md)
- [08 - SystemParts G1 Modules](./08-systemparts-g1-modules.md)
- [09 - SystemParts G2 Modules](./09-systemparts-g2-modules.md)
- [10 - SystemParts G3 Modules](./10-systemparts-g3-modules.md)
- [11 - Integration Suite And External Services](./11-integration-suite-and-external-services.md)
- [12 - Branch Locator Module](./12-branch-locator-module.md)
- [13 - Background Agent And Job Execution](./13-background-agent-and-job-execution.md)
- [14 - Database Assets Setup And Recovery](./14-database-assets-setup-and-recovery.md)
- [15 - Build Deploy And Local Run Workflow](./15-build-deploy-and-local-run-workflow.md)
- [16 - Standalone App BibleReader](./16-standalone-app-biblereader.md)
- [17 - Standalone App LessonReviewer](./17-standalone-app-lessonreviewer.md)
- [18 - Utilities And Tooling](./18-utilities-and-tooling.md)
- [19 - Testing Quality And Risk Evaluation](./19-testing-quality-and-risk-evaluation.md)
