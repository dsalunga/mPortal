# LGC-037 - WCMS.Framework

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-037 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Framework` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 1 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 230 |

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

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Agent :: AgentTaskBase | `legacy/Portal/WebSystem/WCMS.Framework/Agent/AgentTaskBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Agent :: FrameworkAgent | `legacy/Portal/WebSystem/WCMS.Framework/Agent/FrameworkAgent.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Agent :: ITask | `legacy/Portal/WebSystem/WCMS.Framework/Agent/ITask.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Agent :: ITaskRequest | `legacy/Portal/WebSystem/WCMS.Framework/Agent/ITaskRequest.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Caching :: DataSetCache | `legacy/Portal/WebSystem/WCMS.Framework/Caching/DataSetCache.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Caching :: ICacheManager | `legacy/Portal/WebSystem/WCMS.Framework/Caching/ICacheManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Caching :: ICacheable | `legacy/Portal/WebSystem/WCMS.Framework/Caching/ICacheable.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Caching :: MemoryCache | `legacy/Portal/WebSystem/WCMS.Framework/Caching/MemoryCache.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Caching :: NullCache | `legacy/Portal/WebSystem/WCMS.Framework/Caching/NullCache.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: Constants | `legacy/Portal/WebSystem/WCMS.Framework/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: INameWebObject | `legacy/Portal/WebSystem/WCMS.Framework/Core/INameWebObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: IQueryFilterElement | `legacy/Portal/WebSystem/WCMS.Framework/Core/IQueryFilterElement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: ISelfManager | `legacy/Portal/WebSystem/WCMS.Framework/Core/ISelfManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: IVersionDataManager | `legacy/Portal/WebSystem/WCMS.Framework/Core/IVersionDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: IVersionDataProvider | `legacy/Portal/WebSystem/WCMS.Framework/Core/IVersionDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: IVersionWebObject | `legacy/Portal/WebSystem/WCMS.Framework/Core/IVersionWebObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: IWebHeaderTarget | `legacy/Portal/WebSystem/WCMS.Framework/Core/IWebHeaderTarget.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: LinkedPart | `legacy/Portal/WebSystem/WCMS.Framework/Core/LinkedPart.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: ModelBase | `legacy/Portal/WebSystem/WCMS.Framework/Core/ModelBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: NamedWebObject | `legacy/Portal/WebSystem/WCMS.Framework/Core/NamedWebObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: ObjectCapsule | `legacy/Portal/WebSystem/WCMS.Framework/Core/ObjectCapsule.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: ObjectColumnAttribute | `legacy/Portal/WebSystem/WCMS.Framework/Core/ObjectColumnAttribute.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: ObjectManager | `legacy/Portal/WebSystem/WCMS.Framework/Core/ObjectManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: PageElementBase | `legacy/Portal/WebSystem/WCMS.Framework/Core/PageElementBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: ParameterizedWebObject | `legacy/Portal/WebSystem/WCMS.Framework/Core/ParameterizedWebObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: QueryFilter | `legacy/Portal/WebSystem/WCMS.Framework/Core/QueryFilter.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: QueryFilterElement | `legacy/Portal/WebSystem/WCMS.Framework/Core/QueryFilterElement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core/Shared :: Country | `legacy/Portal/WebSystem/WCMS.Framework/Core/Shared/Country.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core/Shared :: CountryProvider | `legacy/Portal/WebSystem/WCMS.Framework/Core/Shared/CountryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core/Shared :: CountryState | `legacy/Portal/WebSystem/WCMS.Framework/Core/Shared/CountryState.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core/Shared :: CountryStateProvider | `legacy/Portal/WebSystem/WCMS.Framework/Core/Shared/CountryStateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core/Shared :: ICountryProvider | `legacy/Portal/WebSystem/WCMS.Framework/Core/Shared/ICountryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: StandardDataManager | `legacy/Portal/WebSystem/WCMS.Framework/Core/StandardDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: StandardVersionDataManager | `legacy/Portal/WebSystem/WCMS.Framework/Core/StandardVersionDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebAttachment | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebAttachment.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebComment | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebComment.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebConstant | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebConstant.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebDirectoryEntry | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebDirectoryEntry.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebFile | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebFile.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebFolder | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebFolder.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebJob | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebJob.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObject | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectAttribute | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectAttribute.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectBase | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectHeader | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectHeader.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectIPAddress | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectIPAddress.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectLink | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectLink.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectSecurity | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectSecurity.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebObjectSecurityPermission | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObjectSecurityPermission.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebOffice | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebOffice.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebParameter | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebParameter.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebParameterSet | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebParameterSet.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebRegistry | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebRegistry.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebSubscription | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebSubscription.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: WebTextResource | `legacy/Portal/WebSystem/WCMS.Framework/Core/WebTextResource.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Core :: XmlDataProvider | `legacy/Portal/WebSystem/WCMS.Framework/Core/XmlDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: DataAccess | `legacy/Portal/WebSystem/WCMS.Framework/Data/DataAccess.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: DataSource | `legacy/Portal/WebSystem/WCMS.Framework/Data/DataSource.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: DataStoreEntity | `legacy/Portal/WebSystem/WCMS.Framework/Data/DataStoreEntity.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: DataStoreManager | `legacy/Portal/WebSystem/WCMS.Framework/Data/DataStoreManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: GenericSqlDataProvider | `legacy/Portal/WebSystem/WCMS.Framework/Data/GenericSqlDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: GenericSqlDataProviderBase | `legacy/Portal/WebSystem/WCMS.Framework/Data/GenericSqlDataProviderBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: IDataManager | `legacy/Portal/WebSystem/WCMS.Framework/Data/IDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: IDataProvider | `legacy/Portal/WebSystem/WCMS.Framework/Data/IDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: IDataSourceManager | `legacy/Portal/WebSystem/WCMS.Framework/Data/IDataSourceManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: SqlDataProviderBase | `legacy/Portal/WebSystem/WCMS.Framework/Data/SqlDataProviderBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: SqlQueryGenerator | `legacy/Portal/WebSystem/WCMS.Framework/Data/SqlQueryGenerator.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Data :: VersionSqlDataProvider | `legacy/Portal/WebSystem/WCMS.Framework/Data/VersionSqlDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Diagnostics :: EventLog | `legacy/Portal/WebSystem/WCMS.Framework/Diagnostics/EventLog.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Diagnostics :: IEventLogProvider | `legacy/Portal/WebSystem/WCMS.Framework/Diagnostics/IEventLogProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Diagnostics :: PerformanceLog | `legacy/Portal/WebSystem/WCMS.Framework/Diagnostics/PerformanceLog.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/IO :: PathResolver | `legacy/Portal/WebSystem/WCMS.Framework/IO/PathResolver.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IPageElement | `legacy/Portal/WebSystem/WCMS.Framework/IPageElement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: ISharableContent | `legacy/Portal/WebSystem/WCMS.Framework/ISharableContent.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebAttachmentProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebAttachmentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebCategoryProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebCategoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebCommentProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebCommentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebConstantProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebConstantProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebFileProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebFileProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebFolderProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebFolderProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebJobProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebJobProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebObject | `legacy/Portal/WebSystem/WCMS.Framework/IWebObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebObjectHeaderProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebObjectHeaderProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebObjectIPAddressProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebObjectIPAddressProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebObjectProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebParameterProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebParameterProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebParameterSetProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebParameterSetProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebRegistryProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebRegistryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebSiteIdentityProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebSiteIdentityProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebSubscriptionProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebSubscriptionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: IWebTextResourceProvider | `legacy/Portal/WebSystem/WCMS.Framework/IWebTextResourceProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: UserProviderManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/UserProviderManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebConstantManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebConstantManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebGlobalPolicyManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebGlobalPolicyManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebGroupManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebGroupManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebMasterPageManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebMasterPageManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebObjectHeaderManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebObjectHeaderManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebObjectManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebObjectManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebObjectSecurityManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebObjectSecurityManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebObjectSecurityPermissionManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebObjectSecurityPermissionManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPageElementManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPageElementManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPageManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPageManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPagePanelManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPagePanelManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebParameterManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebParameterManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebParameterSetManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebParameterSetManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPartControlManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPartControlManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPartControlTemplateManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPartControlTemplateManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPartManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPartManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebPermissionManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebPermissionManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebRegistryManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebRegistryManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebShareManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebShareManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebShortUrlManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebShortUrlManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebSiteIdentityManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebSiteIdentityManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebSiteManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebSiteManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebSkinManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebSkinManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebSubscriptionManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebSubscriptionManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebTemplateManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebTemplateManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebTemplatePanelManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebTemplatePanelManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebTextResourceManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebTextResourceManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebThemeManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebThemeManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebUserGroupManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebUserGroupManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Manager :: WebUserManager | `legacy/Portal/WebSystem/WCMS.Framework/Manager/WebUserManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: AttachmentManager | `legacy/Portal/WebSystem/WCMS.Framework/Net/AttachmentManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: CmsEmail | `legacy/Portal/WebSystem/WCMS.Framework/Net/CmsEmail.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: FileSyncInfo | `legacy/Portal/WebSystem/WCMS.Framework/Net/FileSyncInfo.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: IWebMessageQueueProvider | `legacy/Portal/WebSystem/WCMS.Framework/Net/IWebMessageQueueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: MessageProcessorTask | `legacy/Portal/WebSystem/WCMS.Framework/Net/MessageProcessorTask.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: SmsConfig | `legacy/Portal/WebSystem/WCMS.Framework/Net/SmsConfig.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: SmsMessage | `legacy/Portal/WebSystem/WCMS.Framework/Net/SmsMessage.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: WebMailMessage | `legacy/Portal/WebSystem/WCMS.Framework/Net/WebMailMessage.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Net :: WebMessageQueue | `legacy/Portal/WebSystem/WCMS.Framework/Net/WebMessageQueue.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: ObjectKey | `legacy/Portal/WebSystem/WCMS.Framework/ObjectKey.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: ObjectRecordPair | `legacy/Portal/WebSystem/WCMS.Framework/ObjectRecordPair.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: IPartDataManager | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/IPartDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: IWebPartAdminProvider | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/IWebPartAdminProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: IWebPartConfigProvider | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/IWebPartConfigProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: IWebPartControlProvider | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/IWebPartControlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: IWebPartControlTemplateProvider | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/IWebPartControlTemplateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: IWebPartProvider | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/IWebPartProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: PartDataManagerModel | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/PartDataManagerModel.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: WebPart | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/WebPart.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: WebPartAdmin | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/WebPartAdmin.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: WebPartConfig | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/WebPartConfig.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: WebPartControl | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/WebPartControl.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/PartModel :: WebPartControlTemplate | `legacy/Portal/WebSystem/WCMS.Framework/PartModel/WebPartControlTemplate.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IPublicSecurable | `legacy/Portal/WebSystem/WCMS.Framework/Security/IPublicSecurable.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IUserProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IUserProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IUserProviderProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IUserProviderProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebAccount | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebAccount.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebAddressProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebAddressProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebGlobalPolicyProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebGlobalPolicyProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebGroupProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebGroupProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebObjectSecurityPermissionProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebObjectSecurityPermissionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebObjectSecurityProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebObjectSecurityProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebPermissionProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebPermissionProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebPermissionSetProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebPermissionSetProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebRoleProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebRoleProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebShareProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebShareProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebUserGroupProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebUserGroupProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebUserProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebUserProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: IWebUserRoleProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/IWebUserRoleProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: LoginCookieManager | `legacy/Portal/WebSystem/WCMS.Framework/Security/LoginCookieManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: OtpCache | `legacy/Portal/WebSystem/WCMS.Framework/Security/OtpCache.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: OtpCodeGenerator | `legacy/Portal/WebSystem/WCMS.Framework/Security/OtpCodeGenerator.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: PublicSecurableObject | `legacy/Portal/WebSystem/WCMS.Framework/Security/PublicSecurableObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: SecurableObject | `legacy/Portal/WebSystem/WCMS.Framework/Security/SecurableObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: UserInfo | `legacy/Portal/WebSystem/WCMS.Framework/Security/UserInfo.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: UserProvider | `legacy/Portal/WebSystem/WCMS.Framework/Security/UserProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: UserSession | `legacy/Portal/WebSystem/WCMS.Framework/Security/UserSession.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: UserSessionBrowser | `legacy/Portal/WebSystem/WCMS.Framework/Security/UserSessionBrowser.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: UserSessionManager | `legacy/Portal/WebSystem/WCMS.Framework/Security/UserSessionManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WSUserInfo | `legacy/Portal/WebSystem/WCMS.Framework/Security/WSUserInfo.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebAddress | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebAddress.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebGlobalPolicy | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebGlobalPolicy.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebGroup | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebGroup.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebPermission | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebPermission.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebPermissionSet | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebPermissionSet.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebRole | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebRole.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebShare | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebShare.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebUser | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebUser.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Security :: WebUserGroup | `legacy/Portal/WebSystem/WCMS.Framework/Security/WebUserGroup.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebMasterPageProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebMasterPageProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebPageElementProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebPageElementProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebPagePanelProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebPagePanelProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebPageProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebPageProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebShortUrlProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebShortUrlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebSiteProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebSiteProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebSkinProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebSkinProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebTemplatePanelProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebTemplatePanelProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebTemplateProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebTemplateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: IWebThemeProvider | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/IWebThemeProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebContextBase | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebContextBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebMasterPage | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebMasterPage.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebObjectItem | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebObjectItem.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebPage | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebPage.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebPageElement | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebPageElement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebPagePanel | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebPagePanel.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebShortUrl | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebShortUrl.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebSite | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebSite.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebSiteIdentity | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebSiteIdentity.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebSkin | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebSkin.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebTemplate | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebTemplate.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebTemplatePanel | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebTemplatePanel.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/SiteModel :: WebTheme | `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebTheme.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: AccountHelper | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/AccountHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: HtmlHelper | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/HtmlHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: LoginSecurity | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: ParameterHelper | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/ParameterHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: SecurityHelper | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/SecurityHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: SyncHelper | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/SyncHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: UserDataTag | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/UserDataTag.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: UserIdEqualityComparer | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/UserIdEqualityComparer.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: UserNameEqualityComparer | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/UserNameEqualityComparer.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: UserTagEqualityComparer | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/UserTagEqualityComparer.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: WHelper | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/WHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: WebCryptography | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/WebCryptography.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: WebRedirector | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/WebRedirector.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: WebSetup | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/WebSetup.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework/Utilities :: WebSubstituter | `legacy/Portal/WebSystem/WCMS.Framework/Utilities/WebSubstituter.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WApproval | `legacy/Portal/WebSystem/WCMS.Framework/WApproval.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WApprovalPartial | `legacy/Portal/WebSystem/WCMS.Framework/WApprovalPartial.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WConfig | `legacy/Portal/WebSystem/WCMS.Framework/WConfig.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WContext | `legacy/Portal/WebSystem/WCMS.Framework/WContext.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WFrameworkModel.Context | `legacy/Portal/WebSystem/WCMS.Framework/WFrameworkModel.Context.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WFrameworkModel | `legacy/Portal/WebSystem/WCMS.Framework/WFrameworkModel.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WQuery | `legacy/Portal/WebSystem/WCMS.Framework/WQuery.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WSession | `legacy/Portal/WebSystem/WCMS.Framework/WSession.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WebCategory | `legacy/Portal/WebSystem/WCMS.Framework/WebCategory.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WebRewriter | `legacy/Portal/WebSystem/WCMS.Framework/WebRewriter.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.Framework :: WebUserContainer | `legacy/Portal/WebSystem/WCMS.Framework/WebUserContainer.cs` | Library/business component; assess API compatibility and dependencies. |
