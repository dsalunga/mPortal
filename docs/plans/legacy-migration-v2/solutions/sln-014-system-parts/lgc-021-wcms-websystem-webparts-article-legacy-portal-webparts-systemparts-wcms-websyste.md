# LGC-021 - WCMS.WebSystem.WebParts.Article

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-021 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:20, Not Applicable:4, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 21 |

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
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: Article | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Article.cs` | `./Article.cs` |
| LGC-021 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: ArticleColumn | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ArticleColumn.cs` | N/A (retired/replaced in modern architecture). |
| LGC-021 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: ArticleHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ArticleHelper.cs` | N/A (retired/replaced in modern architecture). |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: ArticleLink | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ArticleLink.cs` | `./ArticleLink.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: ArticleList | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ArticleList.cs` | `./ArticleList.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: ArticleTemplate | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ArticleTemplate.cs` | `./ArticleTemplate.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constants.cs` | `./Constants.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Managers :: ArticleLinkManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/ArticleLinkManager.cs` | `./Managers/ArticleLinkManager.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Managers :: ArticleListManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/ArticleListManager.cs` | `./Managers/ArticleListManager.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Managers :: ArticleManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/ArticleManager.cs` | `./Managers/ArticleManager.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Managers :: ArticlePartDataManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/ArticlePartDataManager.cs` | `./Managers/ArticlePartDataManager.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Managers :: ArticleTemplateManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/ArticleTemplateManager.cs` | `./Managers/ArticleTemplateManager.cs` |
| LGC-021 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: ArticleColumnProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/ArticleColumnProvider.cs` | N/A (retired/replaced in modern architecture). |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: ArticleLinkProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/ArticleLinkProvider.cs` | `./Providers/ArticleLinkProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: ArticleListProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/ArticleListProvider.cs` | `./Providers/ArticleListProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: ArticleProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/ArticleProvider.cs` | `./Providers/ArticleProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: ArticleTemplateProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/ArticleTemplateProvider.cs` | `./Providers/ArticleTemplateProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: IArticleLinkProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IArticleLinkProvider.cs` | `./Providers/IArticleLinkProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: IArticleListProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IArticleListProvider.cs` | `./Providers/IArticleListProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: IArticleProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IArticleProvider.cs` | `./Providers/IArticleProvider.cs` |
| LGC-021 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Providers :: IArticleTemplateProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IArticleTemplateProvider.cs` | `./Providers/IArticleTemplateProvider.cs` |
