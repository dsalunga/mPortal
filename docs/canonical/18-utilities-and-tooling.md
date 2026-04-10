# 18 - Utilities And Tooling

## Scope
`legacy/Portal/Utilities` and utility tasks under `WCMS.WebSystem.Utilities`.

## Utility inventory
- `DbManager` (console): backup/restore CLI wrapper over framework DB manager.
- `WebSystemDeployer` (WinForms): file-list based copy/deploy helper between source/target roots.
- `PostBuildManager` (console): intended part/bin merge utility for module outputs.
- `WebExtractor` (console): archive extraction helper with delete-after-extract behavior.
- `MySQL TableEditor` (WinForms): database table browser/editor with export actions.
- `DbManagerWPF` (WPF): scaffolded shell with minimal implementation.

## Framework utility tasks
- `EmailUpdaterTask`: XML-driven bulk user email migration task.
- `SenderTask`: template-based bulk message sender with configurable interval.
- Shared `DbManager` library class handles schema/data restore, backup, and drop operations.

## Operational role in ecosystem
- Tools bridge gaps in automated CI/CD by supporting manual maintenance workflows.
- Utilities assume local workstation execution and direct filesystem/database access.
- Several scripts rely on these binaries from `Portal/Binaries` during setup/restore.

## Evaluation
Strengths:
- Practical maintenance coverage for legacy operational needs.
- Reusable helper patterns around XML, file copy, and DB automation.

Risks:
- Tool quality/maturity is uneven (some full-featured, others skeleton or legacy).
- Limited guardrails for destructive operations in database and filesystem flows.
- Tight coupling to legacy paths and environment assumptions.

## Key anchors
- `legacy/Portal/Utilities/DbManager/DbManager/Program.cs`
- `legacy/Portal/Utilities/WebSystemDeployer/WebSystemDeployer/FormMain.cs`
- `legacy/Portal/Utilities/PostBuildManager/PostBuildManager/Program.cs`
- `legacy/Portal/Utilities/WebExtractor/WebExtractor/Program.cs`
- `legacy/Portal/Utilities/MySQL TableEditor/Form1.cs`
- `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/DbManager.cs`
- `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/EmailUpdater/EmailUpdaterTask.cs`
- `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/Spammer/SenderTask.cs`

