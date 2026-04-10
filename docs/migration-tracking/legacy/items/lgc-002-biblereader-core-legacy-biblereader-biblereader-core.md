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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleBook | `legacy/BibleReader/BibleReader.Core/BibleBook.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleBookName | `legacy/BibleReader/BibleReader.Core/BibleBookName.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleChapter | `legacy/BibleReader/BibleReader.Core/BibleChapter.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleManager | `legacy/BibleReader/BibleReader.Core/BibleManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVerse | `legacy/BibleReader/BibleReader.Core/BibleVerse.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVersion | `legacy/BibleReader/BibleReader.Core/BibleVersion.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVersionLanguage | `legacy/BibleReader/BibleReader.Core/BibleVersionLanguage.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core :: Constants | `legacy/BibleReader/BibleReader.Core/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleBookNameProvider | `legacy/BibleReader/BibleReader.Core/Providers/BibleBookNameProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleVersionLanguageProvider | `legacy/BibleReader/BibleReader.Core/Providers/BibleVersionLanguageProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleVersionProvider | `legacy/BibleReader/BibleReader.Core/Providers/BibleVersionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-002 | Not Stated | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: GenericBibleVerseProvider | `legacy/BibleReader/BibleReader.Core/Providers/GenericBibleVerseProvider.cs` | Library/business component; assess API compatibility and dependencies. |
