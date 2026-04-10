# LGC-016 - WCMS.WebSystem.Apps.Integration.UnitTest

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-016 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` |
| Project Directory | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 2 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj |
| ../WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-016 | Partial | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest :: MemberHelperTests | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/MemberHelperTests.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-016 | Partial | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest :: UnitTest1 | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/UnitTest1.cs` | Library/business component; assess API compatibility and dependencies. |
