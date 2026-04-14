# LGC-014 - WCMS.WebSystem.Apps.BibleReader

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-014 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` |
| Project Directory | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 11 |

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
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: AppConfig | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/AppConfig.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleAccess | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/BibleAccess.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleAccessResult | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/BibleAccessResult.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleUserSession | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/BibleUserSession.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleVersionAccess | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/BibleVersionAccess.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleVersionAccessResult | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/BibleVersionAccessResult.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Constants.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: BibleAccessSqlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers/BibleAccessSqlProvider.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: BibleVersionAccessSqlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers/BibleVersionAccessSqlProvider.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: IBibleAccessProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers/IBibleAccessProvider.cs` |
| LGC-014 | Not Stated | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: IBibleVersionAccessProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers/IBibleVersionAccessProvider.cs` |
