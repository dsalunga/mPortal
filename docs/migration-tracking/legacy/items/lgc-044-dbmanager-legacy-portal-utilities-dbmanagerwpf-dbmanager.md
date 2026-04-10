# LGC-044 - DbManager

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-044 |
| Project Type | Utility |
| Project File | `legacy/Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` |
| Project Directory | `legacy/Portal/Utilities/DbManagerWPF/DbManager` |
| Output Type | WinExe |
| Target Framework | v4.0 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.0). |
| Project References | 0 |
| Surface Artifacts | 2 |
| Component/Class Artifacts | 2 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

| Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- |
| XAML View | legacy/Portal/Utilities/DbManagerWPF/DbManager :: App.xaml | `legacy/Portal/Utilities/DbManagerWPF/DbManager/App.xaml` | `legacy/Portal/Utilities/DbManagerWPF/DbManager/App.xaml.cs` | Desktop UI artifact; assess target desktop strategy. |
| XAML View | legacy/Portal/Utilities/DbManagerWPF/DbManager :: MainWindow.xaml | `legacy/Portal/Utilities/DbManagerWPF/DbManager/MainWindow.xaml` | `legacy/Portal/Utilities/DbManagerWPF/DbManager/MainWindow.xaml.cs` | Desktop UI artifact; assess target desktop strategy. |

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/Utilities/DbManagerWPF/DbManager :: App | `legacy/Portal/Utilities/DbManagerWPF/DbManager/App.xaml.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/Utilities/DbManagerWPF/DbManager :: MainWindow | `legacy/Portal/Utilities/DbManagerWPF/DbManager/MainWindow.xaml.cs` | Library/business component; assess API compatibility and dependencies. |
