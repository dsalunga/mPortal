# LGC-044 - DbManager

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-044 |
| Project Type | Utility |
| Project File | `legacy/Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` |
| Modern Project File / Evidence | `Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` |
| Project Directory | `legacy/Portal/Utilities/DbManagerWPF/DbManager` |
| Output Type | WinExe |
| Target Framework | v4.0 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:8, Not Applicable:0, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 2 |
| Component/Class Artifacts | 2 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-044 | Completed | XAML View | legacy/Portal/Utilities/DbManagerWPF/DbManager :: App.xaml | Desktop UI artifact; assess target desktop strategy. | `./App.xaml` | `./App.xaml` | `./App.xaml.cs` |
| LGC-044 | Completed | XAML View | legacy/Portal/Utilities/DbManagerWPF/DbManager :: MainWindow.xaml | Desktop UI artifact; assess target desktop strategy. | `./MainWindow.xaml` | `./MainWindow.xaml` | `./MainWindow.xaml.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-044 | Completed | Class Component | legacy/Portal/Utilities/DbManagerWPF/DbManager :: App | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./App.xaml.cs` | `./App.xaml.cs` |
| LGC-044 | Completed | Class Component | legacy/Portal/Utilities/DbManagerWPF/DbManager :: MainWindow | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./MainWindow.xaml.cs` | `./MainWindow.xaml.cs` |
