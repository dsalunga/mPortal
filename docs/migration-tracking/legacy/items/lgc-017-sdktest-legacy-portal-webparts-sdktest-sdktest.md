# LGC-017 - SDKTest

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-017 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SDKTest/SDKTest/SDKTest.csproj` |
| Project Directory | `legacy/Portal/WebParts/SDKTest/SDKTest` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 0 |
| Surface Artifacts | 1 |
| Component/Class Artifacts | 1 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

_No artifacts found._

## User Controls

| Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- |
| User Control | legacy/Portal/WebParts/SDKTest/SDKTest/WebParts/Test :: WebUserControl1.ascx | `legacy/Portal/WebParts/SDKTest/SDKTest/WebParts/Test/WebUserControl1.ascx` | `legacy/Portal/WebParts/SDKTest/SDKTest/WebParts/Test/WebUserControl1.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebParts/SDKTest/SDKTest/WebParts/Test :: WebUserControl1 | `legacy/Portal/WebParts/SDKTest/SDKTest/WebParts/Test/WebUserControl1.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
