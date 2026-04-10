# LGC-045 - TableEditor

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-045 |
| Project Type | Utility |
| Project File | `legacy/Portal/Utilities/MySQL TableEditor/TableEditor.csproj` |
| Project Directory | `legacy/Portal/Utilities/MySQL TableEditor` |
| Output Type | WinExe |
| Target Framework | v4.7 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.7). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 1 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/Utilities/MySQL TableEditor :: Form1 | `legacy/Portal/Utilities/MySQL TableEditor/Form1.cs` | Library/business component; assess API compatibility and dependencies. |
