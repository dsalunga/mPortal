# LGC-038 - WCMS.Framework.Core.SqlProvider

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-038 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 48 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: Class1 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Class1.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Diagnostics :: EventLogProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Diagnostics/EventLogProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: UserProviderProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/UserProviderProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebAddressProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebAddressProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebAttachmentProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebAttachmentProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebCategoryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebCategoryProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebCommentProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebCommentProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebConstantProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebConstantProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebFileProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebFileProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebFolderProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebFolderProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebGlobalPolicyProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebGlobalPolicyProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebGroupProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebGroupProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebJobProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebJobProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMasterPageItemProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebMasterPageItemProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMasterPageProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebMasterPageProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMessageQueueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebMessageQueueProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectHeaderProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectHeaderProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectIPAddressProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectIPAddressProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectSecurityPermissionProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectSecurityPermissionProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectSecurityProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectSecurityProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPageElementProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPageElementProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPagePanelProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPagePanelProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPageProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPageProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebParameterProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebParameterProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebParameterSetProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebParameterSetProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartAdminProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartAdminProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartConfigProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartConfigProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartControlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartControlProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartControlTemplateProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartControlTemplateProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPermissionProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPermissionProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPermissionSetProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPermissionSetProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebRegistryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebRegistryProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebRoleProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebRoleProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebShareProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebShareProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebShortUrlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebShortUrlProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSiteIdentityProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSiteIdentityProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSiteProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSiteProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSkinProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSkinProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSubscriptionProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSubscriptionProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTemplatePanelProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebTemplatePanelProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTemplateProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebTemplateProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTextResourceProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebTextResourceProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebThemeProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebThemeProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserGroupProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebUserGroupProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebUserProvider.cs` |
| LGC-038 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserRoleProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebUserRoleProvider.cs` |
