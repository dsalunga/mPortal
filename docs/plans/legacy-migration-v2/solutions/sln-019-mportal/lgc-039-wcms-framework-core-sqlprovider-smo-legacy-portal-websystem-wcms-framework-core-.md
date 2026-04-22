# LGC-039 - WCMS.Framework.Core.SqlProvider.Smo

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-039 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` |
| Modern Project File / Evidence | `Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:3, Not Applicable:1, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 1 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-039 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo :: SqlScriptGenerator | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./SqlScriptGenerator.cs` | `./SqlScriptGenerator.cs` |
