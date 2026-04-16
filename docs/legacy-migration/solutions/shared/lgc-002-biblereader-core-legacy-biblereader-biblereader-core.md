# LGC-002 - BibleReader.Core

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-002 |
| Project Type | App |
| Project File | `legacy/BibleReader/BibleReader.Core/BibleReader.Core.csproj` |
| Modern Project File / Evidence | `Apps/BibleReader/BibleReader.Core/BibleReader.Core.csproj` |
| Project Directory | `legacy/BibleReader/BibleReader.Core` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:14, Not Applicable:0, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 12 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleBook | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleBook.cs` | `Apps/BibleReader/BibleReader.Core/BibleBook.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleBookName | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleBookName.cs` | `Apps/BibleReader/BibleReader.Core/BibleBookName.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleChapter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleChapter.cs` | `Apps/BibleReader/BibleReader.Core/BibleChapter.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleManager.cs` | `Apps/BibleReader/BibleReader.Core/BibleManager.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVerse | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleVerse.cs` | `Apps/BibleReader/BibleReader.Core/BibleVerse.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVersion | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleVersion.cs` | `Apps/BibleReader/BibleReader.Core/BibleVersion.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: BibleVersionLanguage | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/BibleVersionLanguage.cs` | `Apps/BibleReader/BibleReader.Core/BibleVersionLanguage.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/Constants.cs` | `Apps/BibleReader/BibleReader.Core/Constants.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleBookNameProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/Providers/BibleBookNameProvider.cs` | `Apps/BibleReader/BibleReader.Core/Providers/BibleBookNameProvider.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleVersionLanguageProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/Providers/BibleVersionLanguageProvider.cs` | `Apps/BibleReader/BibleReader.Core/Providers/BibleVersionLanguageProvider.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: BibleVersionProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/Providers/BibleVersionProvider.cs` | `Apps/BibleReader/BibleReader.Core/Providers/BibleVersionProvider.cs` |
| LGC-002 | Completed | Class Component | legacy/BibleReader/BibleReader.Core/Providers :: GenericBibleVerseProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader.Core/Providers/GenericBibleVerseProvider.cs` | `Apps/BibleReader/BibleReader.Core/Providers/GenericBibleVerseProvider.cs` |
