# LGC-002 - BibleReader.Core

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-002 |
| Project Type | App |
| Project File | `legacy/BibleReader/BibleReader.Core/BibleReader.Core.csproj` |
| Project Directory | `legacy/BibleReader/BibleReader.Core` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 12 |

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
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleBook | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleBook.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleBookName | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleBookName.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleChapter | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleChapter.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleManager | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleManager.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVerse | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleVerse.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVersion | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleVersion.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVersionLanguage | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/BibleVersionLanguage.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/Constants.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleBookNameProvider | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/Providers/BibleBookNameProvider.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleVersionLanguageProvider | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/Providers/BibleVersionLanguageProvider.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleVersionProvider | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/Providers/BibleVersionProvider.cs` |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: GenericBibleVerseProvider | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader.Core/Providers/GenericBibleVerseProvider.cs` |
