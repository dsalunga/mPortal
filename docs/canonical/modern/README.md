# Modern Canonical Cards

This folder contains target-state canonical cards for the modernized platform.

Snapshot:
- Date: 2026-04-14
- Runtime target: `.NET 10`
- Program direction: staged strangler migration from `legacy/` to modern host/module architecture
- Editor policy: `FredCK.FCKeditorV2` is **Do Not Migrate As-Is**; replace all usage with `TipTap OSS` + server-side HTML sanitization

Confirmed architecture decisions:
- CMS module manifest contract: `Hybrid DB + code manifest`.
- Admin modernization strategy: `Hybrid per domain` (`Razor-first` for CMS/admin-heavy surfaces, `API + SPA` where interaction complexity warrants it).
- Cutover strategy: `Capability-by-capability staged strangler` (no big-bang cutover).

How to use this set:
1. Start with cards `00` to `07` for platform/runtime/module-loading architecture.
2. Use cards `08` to `12` for feature/module modernization targets.
3. Use cards `13` to `18` for jobs, data, build/release, standalone apps, and utilities.
4. Use card `19` for quality gates and migration risk controls.

Card index:
- [00 - Modern System Overview](./00-legacy-system-overview.md)
- [01 - Modern Solution Topology And Project Map](./01-solution-topology-and-project-map.md)
- [02 - Modern Runtime Request Pipeline And Rendering](./02-runtime-request-pipeline-and-rendering.md)
- [03 - Modern Core Domain Model And WebObject Registry](./03-core-domain-model-and-webobject-registry.md)
- [04 - Modern Data Providers Caching And Persistence](./04-data-providers-caching-and-persistence.md)
- [05 - Modern Security Authentication And Session Control](./05-security-authentication-and-session-control.md)
- [06 - Modern Central Admin Console Capabilities](./06-central-admin-console-capabilities.md)
- [07 - Modern WebPart Platform And Loading Contracts](./07-webpart-platform-and-loading-contracts.md)
- [08 - Modern SystemParts G1 Modules](./08-systemparts-g1-modules.md)
- [09 - Modern SystemParts G2 Modules](./09-systemparts-g2-modules.md)
- [10 - Modern SystemParts G3 Modules](./10-systemparts-g3-modules.md)
- [11 - Modern Integration Suite And External Services](./11-integration-suite-and-external-services.md)
- [12 - Modern Branch Locator Module](./12-branch-locator-module.md)
- [13 - Modern Background Agent And Job Execution](./13-background-agent-and-job-execution.md)
- [14 - Modern Database Assets Setup And Recovery](./14-database-assets-setup-and-recovery.md)
- [15 - Modern Build Deploy And Local Run Workflow](./15-build-deploy-and-local-run-workflow.md)
- [16 - Modern Standalone App BibleReader](./16-standalone-app-biblereader.md)
- [17 - Modern Standalone App LessonReviewer](./17-standalone-app-lessonreviewer.md)
- [18 - Modern Utilities And Tooling](./18-utilities-and-tooling.md)
- [19 - Modern Testing Quality And Risk Evaluation](./19-testing-quality-and-risk-evaluation.md)
