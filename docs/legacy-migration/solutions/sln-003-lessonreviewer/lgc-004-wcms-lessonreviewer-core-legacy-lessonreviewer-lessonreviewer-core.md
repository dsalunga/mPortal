# LGC-004 - WCMS.LessonReviewer.Core

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-004 |
| Project Type | App |
| Project File | `legacy/LessonReviewer/LessonReviewer.Core/LessonReviewer.Core.csproj` |
| Project Directory | `legacy/LessonReviewer/LessonReviewer.Core` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 4 |

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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-004 | Not Stated | Class Component | legacy/LessonReviewer/LessonReviewer.Core :: ConfigManager | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer.Core/ConfigManager.cs` |
| LGC-004 | Not Stated | Class Component | legacy/LessonReviewer/LessonReviewer.Core :: MakeUpServiceSession | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer.Core/MakeUpServiceSession.cs` |
| LGC-004 | Not Stated | Class Component | legacy/LessonReviewer/LessonReviewer.Core :: PlaybackHelper | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer.Core/PlaybackHelper.cs` |
| LGC-004 | Not Stated | Class Component | legacy/LessonReviewer/LessonReviewer.Core :: ServiceDefinition | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer.Core/ServiceDefinition.cs` |
