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

| Reference Include |
| --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: Class1 | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Class1.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Diagnostics :: EventLogProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Diagnostics/EventLogProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: UserProviderProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/UserProviderProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebAddressProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebAddressProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebAttachmentProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebAttachmentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebCategoryProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebCategoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebCommentProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebCommentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebConstantProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebConstantProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebFileProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebFileProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebFolderProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebFolderProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebGlobalPolicyProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebGlobalPolicyProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebGroupProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebGroupProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebJobProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebJobProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMasterPageItemProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebMasterPageItemProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMasterPageProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebMasterPageProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebMessageQueueProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebMessageQueueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectHeaderProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectHeaderProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectIPAddressProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectIPAddressProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectSecurityPermissionProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectSecurityPermissionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebObjectSecurityProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebObjectSecurityProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPageElementProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPageElementProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPagePanelProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPagePanelProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPageProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPageProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebParameterProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebParameterProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebParameterSetProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebParameterSetProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartAdminProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartAdminProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartConfigProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartConfigProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartControlProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartControlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartControlTemplateProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartControlTemplateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPartProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPartProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPermissionProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPermissionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebPermissionSetProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebPermissionSetProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebRegistryProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebRegistryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebRoleProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebRoleProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebShareProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebShareProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebShortUrlProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebShortUrlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSiteIdentityProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSiteIdentityProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSiteProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSiteProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSkinProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSkinProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebSubscriptionProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebSubscriptionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTemplatePanelProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebTemplatePanelProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTemplateProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebTemplateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebTextResourceProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebTextResourceProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebThemeProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebThemeProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserGroupProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebUserGroupProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebUserProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider :: WebUserRoleProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WebUserRoleProvider.cs` | Library/business component; assess API compatibility and dependencies. |
