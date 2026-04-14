# 14 - Database Assets Setup And Recovery

## Scope
`legacy/Portal/Binaries/Database`, `legacy/Portal/Binaries/WebObject.xml`, and DB manager tooling in `WCMS.WebSystem.Utilities`.

## Asset inventory snapshot
- `WebObject.xml` contains 137 registered object definitions.
- Table scripts: 139 `*.create.sql` + 139 `*.drop.sql`.
- Procedure scripts: 333 `*.create.sql` + 333 `*.drop.sql`.
- DB bootstrap payloads are mirrored under `Portal/Binaries/Database`.

## Recovery workflow
- `FIRST-TIME-setup.cmd` chains:
1. `_create-junction.cmd`
2. `_first-time-build.cmd`
3. `_db-restore-silent.cmd`
- `db-restore.cmd` and `_db-restore-silent.cmd` call `Binaries/DbManager/DbManager.exe /restore`.
- `DbManager` executes schema scripts first, then restores table XML data per `WebObject` order.

## Data and schema orchestration
- `DbManager.RestoreAllObjects()` applies all table/procedure create scripts, then restores XML snapshots.
- `SqlScriptGenerator` can create DB if missing and script table definitions via SMO.
- Object-level restore/drop operations exist (`RestoreObjectSchema`, `DropObjectSchema`).

## Safety model
- SQL script execution continues on error by default in many paths.
- Script batching splits on `\r\nGO`, which is sensitive to formatting variations.
- No transaction wrapper across full restore lifecycle.

## Evaluation
Strengths:
- Full offline schema and procedure artifact set is present.
- Restore tooling is automation-friendly and script-driven.

Risks:
- Partial-failure scenarios can leave database in mixed state.
- XML data restore is coupled to object metadata order and table shape assumptions.
- Legacy SMO and local SQL Server assumptions limit portability.

## Key anchors
- `legacy/Portal/Binaries/WebObject.xml`
- `legacy/Portal/Binaries/Database/Tables/*`
- `legacy/Portal/Binaries/Database/Procedures/*`
- `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/DbManager.cs`
- `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/SqlScriptGenerator.cs`
- `legacy/Portal/FIRST-TIME-setup.cmd`
- `legacy/Portal/db-restore.cmd`

