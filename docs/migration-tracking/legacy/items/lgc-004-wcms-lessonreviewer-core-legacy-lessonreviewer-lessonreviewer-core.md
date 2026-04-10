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

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/LessonReviewer/LessonReviewer.Core :: ConfigManager | `legacy/LessonReviewer/LessonReviewer.Core/ConfigManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/LessonReviewer/LessonReviewer.Core :: MakeUpServiceSession | `legacy/LessonReviewer/LessonReviewer.Core/MakeUpServiceSession.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/LessonReviewer/LessonReviewer.Core :: PlaybackHelper | `legacy/LessonReviewer/LessonReviewer.Core/PlaybackHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/LessonReviewer/LessonReviewer.Core :: ServiceDefinition | `legacy/LessonReviewer/LessonReviewer.Core/ServiceDefinition.cs` | Library/business component; assess API compatibility and dependencies. |
