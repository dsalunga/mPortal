# LGC-038 - WCMS.Framework.Core.SqlProvider

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-038 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` |
| Modern Project File / Evidence | `Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:47, Not Applicable:3, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 48 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-038 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: Class1 | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Class1.cs` | N/A (retired/replaced in modern architecture). |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Diagnostics :: EventLogProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Diagnostics/EventLogProvider.cs` | `./Diagnostics/EventLogProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: UserProviderProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./UserProviderProvider.cs` | `./UserProviderProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebAddressProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebAddressProvider.cs` | `./WebAddressProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebAttachmentProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebAttachmentProvider.cs` | `./WebAttachmentProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebCategoryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebCategoryProvider.cs` | `./WebCategoryProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebCommentProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebCommentProvider.cs` | `./WebCommentProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebConstantProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebConstantProvider.cs` | `./WebConstantProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebFileProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebFileProvider.cs` | `./WebFileProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebFolderProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebFolderProvider.cs` | `./WebFolderProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebGlobalPolicyProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebGlobalPolicyProvider.cs` | `./WebGlobalPolicyProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebGroupProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebGroupProvider.cs` | `./WebGroupProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebJobProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebJobProvider.cs` | `./WebJobProvider.cs` |
| LGC-038 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMasterPageItemProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebMasterPageItemProvider.cs` | N/A (retired/replaced in modern architecture). |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMasterPageProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebMasterPageProvider.cs` | `./WebMasterPageProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMessageQueueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebMessageQueueProvider.cs` | `./WebMessageQueueProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectHeaderProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebObjectHeaderProvider.cs` | `./WebObjectHeaderProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectIPAddressProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebObjectIPAddressProvider.cs` | `./WebObjectIPAddressProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebObjectProvider.cs` | `./WebObjectProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectSecurityPermissionProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebObjectSecurityPermissionProvider.cs` | `./WebObjectSecurityPermissionProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectSecurityProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebObjectSecurityProvider.cs` | `./WebObjectSecurityProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPageElementProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPageElementProvider.cs` | `./WebPageElementProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPagePanelProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPagePanelProvider.cs` | `./WebPagePanelProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPageProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPageProvider.cs` | `./WebPageProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebParameterProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebParameterProvider.cs` | `./WebParameterProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebParameterSetProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebParameterSetProvider.cs` | `./WebParameterSetProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartAdminProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPartAdminProvider.cs` | `./WebPartAdminProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartConfigProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPartConfigProvider.cs` | `./WebPartConfigProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartControlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPartControlProvider.cs` | `./WebPartControlProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartControlTemplateProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPartControlTemplateProvider.cs` | `./WebPartControlTemplateProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPartProvider.cs` | `./WebPartProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPermissionProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPermissionProvider.cs` | `./WebPermissionProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPermissionSetProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebPermissionSetProvider.cs` | `./WebPermissionSetProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebRegistryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebRegistryProvider.cs` | `./WebRegistryProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebRoleProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebRoleProvider.cs` | `./WebRoleProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebShareProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebShareProvider.cs` | `./WebShareProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebShortUrlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebShortUrlProvider.cs` | `./WebShortUrlProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSiteIdentityProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebSiteIdentityProvider.cs` | `./WebSiteIdentityProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSiteProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebSiteProvider.cs` | `./WebSiteProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSkinProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebSkinProvider.cs` | `./WebSkinProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSubscriptionProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebSubscriptionProvider.cs` | `./WebSubscriptionProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTemplatePanelProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebTemplatePanelProvider.cs` | `./WebTemplatePanelProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTemplateProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebTemplateProvider.cs` | `./WebTemplateProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTextResourceProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebTextResourceProvider.cs` | `./WebTextResourceProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebThemeProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebThemeProvider.cs` | `./WebThemeProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserGroupProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebUserGroupProvider.cs` | `./WebUserGroupProvider.cs` |
| LGC-038 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebUserProvider.cs` | `./WebUserProvider.cs` |
| LGC-038 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserRoleProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WebUserRoleProvider.cs` | N/A (retired/replaced in modern architecture). |
