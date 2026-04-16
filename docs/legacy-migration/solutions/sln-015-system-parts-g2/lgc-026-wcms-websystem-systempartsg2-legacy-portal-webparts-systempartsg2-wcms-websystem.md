# LGC-026 - WCMS.WebSystem.SystemPartsG2

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-026 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:5, Not Applicable:0, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 3 |

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
| LGC-026 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter :: NewsletterEntry | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/NewsletterEntry.cs` | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/NewsletterEntry.cs` |
| LGC-026 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers :: INewsletterProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers/INewsletterProvider.cs` | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers/INewsletterProvider.cs` |
| LGC-026 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers :: NewsletterSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers/NewsletterSqlProvider.cs` | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers/NewsletterSqlProvider.cs` |
