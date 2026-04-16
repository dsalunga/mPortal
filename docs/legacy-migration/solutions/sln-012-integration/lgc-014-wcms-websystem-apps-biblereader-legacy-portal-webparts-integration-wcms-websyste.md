# LGC-014 - WCMS.WebSystem.Apps.BibleReader

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-014 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` |
| Project Directory | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:13, Not Applicable:0, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 11 |

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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: AppConfig | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppConfig.cs` | `./AppConfig.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleAccess | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./BibleAccess.cs` | `./BibleAccess.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleAccessResult | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./BibleAccessResult.cs` | `./BibleAccessResult.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleUserSession | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./BibleUserSession.cs` | `./BibleUserSession.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleVersionAccess | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./BibleVersionAccess.cs` | `./BibleVersionAccess.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: BibleVersionAccessResult | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./BibleVersionAccessResult.cs` | `./BibleVersionAccessResult.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constants.cs` | `./Constants.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: BibleAccessSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/BibleAccessSqlProvider.cs` | `./Providers/BibleAccessSqlProvider.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: BibleVersionAccessSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/BibleVersionAccessSqlProvider.cs` | `./Providers/BibleVersionAccessSqlProvider.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: IBibleAccessProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IBibleAccessProvider.cs` | `./Providers/IBibleAccessProvider.cs` |
| LGC-014 | Completed | Class Component | legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/Providers :: IBibleVersionAccessProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IBibleVersionAccessProvider.cs` | `./Providers/IBibleVersionAccessProvider.cs` |
