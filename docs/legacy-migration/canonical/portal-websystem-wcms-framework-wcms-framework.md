# P048 - WCMS.Framework

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` |
| Project Kind | Library/Component |
| Assembly Name | `WCMS.Framework` |
| Target Framework | `v4.8` |
| Output Type | `Library` |
| Migration Status | Not Stated |
| Status Basis | No explicit migration metadata or roadmap marker found in project artifact. |
| Target Alternative | TBD |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Provider Component | 56 |
| Manager Component | 44 |
| Task/Job Component | 2 |
| Helper Component | 6 |
| Core Component | 122 |
| Configuration/Resource | 2 |
| Generated UI Partial | 1 |
| Assembly Metadata | 1 |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Generated UI Partial | `(root)` | `WFrameworkModel.Designer` | Not Stated | TBD | `TBD` | `WFrameworkModel.Designer.cs` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `Agent` | `AgentTaskBase` | Blocked | Replace with durable job orchestration and safe cancellation semantics. | Legacy thread/service execution model; requires durable job orchestration redesign. | `Agent/AgentTaskBase.cs` |
| Core Component | `Agent` | `FrameworkAgent` | Blocked | Replace with durable job orchestration and safe cancellation semantics. | Legacy thread/service execution model; requires durable job orchestration redesign. | `Agent/FrameworkAgent.cs` |
| Task/Job Component | `Agent` | `ITask` | Not Stated | TBD | `TBD` | `Agent/ITask.cs` |
| Core Component | `Agent` | `ITaskRequest` | Not Stated | TBD | `TBD` | `Agent/ITaskRequest.cs` |
| Core Component | `Caching` | `DataSetCache` | Not Stated | TBD | `TBD` | `Caching/DataSetCache.cs` |
| Manager Component | `Caching` | `ICacheManager` | Not Stated | TBD | `TBD` | `Caching/ICacheManager.cs` |
| Core Component | `Caching` | `ICacheable` | Not Stated | TBD | `TBD` | `Caching/ICacheable.cs` |
| Core Component | `Caching` | `MemoryCache` | Not Stated | TBD | `TBD` | `Caching/MemoryCache.cs` |
| Core Component | `Caching` | `NullCache` | Not Stated | TBD | `TBD` | `Caching/NullCache.cs` |
| Core Component | `(root)` | `Constants` | Not Stated | TBD | `TBD` | `Constants.cs` |
| Core Component | `Core` | `INameWebObject` | Not Stated | TBD | `TBD` | `Core/INameWebObject.cs` |
| Core Component | `Core` | `IQueryFilterElement` | Not Stated | TBD | `TBD` | `Core/IQueryFilterElement.cs` |
| Manager Component | `Core` | `ISelfManager` | Not Stated | TBD | `TBD` | `Core/ISelfManager.cs` |
| Manager Component | `Core` | `IVersionDataManager` | Not Stated | TBD | `TBD` | `Core/IVersionDataManager.cs` |
| Provider Component | `Core` | `IVersionDataProvider` | Not Stated | TBD | `TBD` | `Core/IVersionDataProvider.cs` |
| Core Component | `Core` | `IVersionWebObject` | Not Stated | TBD | `TBD` | `Core/IVersionWebObject.cs` |
| Core Component | `Core` | `IWebHeaderTarget` | Not Stated | TBD | `TBD` | `Core/IWebHeaderTarget.cs` |
| Core Component | `Core` | `LinkedPart` | Not Stated | TBD | `TBD` | `Core/LinkedPart.cs` |
| Core Component | `Core` | `ModelBase` | Not Stated | TBD | `TBD` | `Core/ModelBase.cs` |
| Core Component | `Core` | `NamedWebObject` | Not Stated | TBD | `TBD` | `Core/NamedWebObject.cs` |
| Core Component | `Core` | `ObjectCapsule` | Not Stated | TBD | `TBD` | `Core/ObjectCapsule.cs` |
| Core Component | `Core` | `ObjectColumnAttribute` | Not Stated | TBD | `TBD` | `Core/ObjectColumnAttribute.cs` |
| Manager Component | `Core` | `ObjectManager` | Not Stated | TBD | `TBD` | `Core/ObjectManager.cs` |
| Core Component | `Core` | `PageElementBase` | Not Stated | TBD | `TBD` | `Core/PageElementBase.cs` |
| Core Component | `Core` | `ParameterizedWebObject` | Not Stated | TBD | `TBD` | `Core/ParameterizedWebObject.cs` |
| Core Component | `Core` | `QueryFilter` | Not Stated | TBD | `TBD` | `Core/QueryFilter.cs` |
| Core Component | `Core` | `QueryFilterElement` | Not Stated | TBD | `TBD` | `Core/QueryFilterElement.cs` |
| Core Component | `Core/Shared` | `Country` | Not Stated | TBD | `TBD` | `Core/Shared/Country.cs` |
| Provider Component | `Core/Shared` | `CountryProvider` | Not Stated | TBD | `TBD` | `Core/Shared/CountryProvider.cs` |
| Core Component | `Core/Shared` | `CountryState` | Not Stated | TBD | `TBD` | `Core/Shared/CountryState.cs` |
| Provider Component | `Core/Shared` | `CountryStateProvider` | Not Stated | TBD | `TBD` | `Core/Shared/CountryStateProvider.cs` |
| Provider Component | `Core/Shared` | `ICountryProvider` | Not Stated | TBD | `TBD` | `Core/Shared/ICountryProvider.cs` |
| Manager Component | `Core` | `StandardDataManager` | Not Stated | TBD | `TBD` | `Core/StandardDataManager.cs` |
| Manager Component | `Core` | `StandardVersionDataManager` | Not Stated | TBD | `TBD` | `Core/StandardVersionDataManager.cs` |
| Core Component | `Core` | `WebAttachment` | Not Stated | TBD | `TBD` | `Core/WebAttachment.cs` |
| Core Component | `Core` | `WebComment` | Not Stated | TBD | `TBD` | `Core/WebComment.cs` |
| Core Component | `Core` | `WebConstant` | Not Stated | TBD | `TBD` | `Core/WebConstant.cs` |
| Core Component | `Core` | `WebDirectoryEntry` | Not Stated | TBD | `TBD` | `Core/WebDirectoryEntry.cs` |
| Core Component | `Core` | `WebFile` | Not Stated | TBD | `TBD` | `Core/WebFile.cs` |
| Core Component | `Core` | `WebFolder` | Not Stated | TBD | `TBD` | `Core/WebFolder.cs` |
| Core Component | `Core` | `WebJob` | Not Stated | TBD | `TBD` | `Core/WebJob.cs` |
| Core Component | `Core` | `WebObject` | Not Stated | TBD | `TBD` | `Core/WebObject.cs` |
| Core Component | `Core` | `WebObjectAttribute` | Not Stated | TBD | `TBD` | `Core/WebObjectAttribute.cs` |
| Core Component | `Core` | `WebObjectBase` | Not Stated | TBD | `TBD` | `Core/WebObjectBase.cs` |
| Core Component | `Core` | `WebObjectHeader` | Not Stated | TBD | `TBD` | `Core/WebObjectHeader.cs` |
| Core Component | `Core` | `WebObjectIPAddress` | Not Stated | TBD | `TBD` | `Core/WebObjectIPAddress.cs` |
| Core Component | `Core` | `WebObjectLink` | Not Stated | TBD | `TBD` | `Core/WebObjectLink.cs` |
| Core Component | `Core` | `WebObjectSecurity` | Not Stated | TBD | `TBD` | `Core/WebObjectSecurity.cs` |
| Core Component | `Core` | `WebObjectSecurityPermission` | Not Stated | TBD | `TBD` | `Core/WebObjectSecurityPermission.cs` |
| Core Component | `Core` | `WebOffice` | Not Stated | TBD | `TBD` | `Core/WebOffice.cs` |
| Core Component | `Core` | `WebParameter` | Not Stated | TBD | `TBD` | `Core/WebParameter.cs` |
| Core Component | `Core` | `WebParameterSet` | Not Stated | TBD | `TBD` | `Core/WebParameterSet.cs` |
| Core Component | `Core` | `WebRegistry` | Not Stated | TBD | `TBD` | `Core/WebRegistry.cs` |
| Core Component | `Core` | `WebSubscription` | Not Stated | TBD | `TBD` | `Core/WebSubscription.cs` |
| Core Component | `Core` | `WebTextResource` | Not Stated | TBD | `TBD` | `Core/WebTextResource.cs` |
| Provider Component | `Core` | `XmlDataProvider` | Not Stated | TBD | `TBD` | `Core/XmlDataProvider.cs` |
| Core Component | `Data` | `DataAccess` | Not Stated | TBD | `TBD` | `Data/DataAccess.cs` |
| Core Component | `Data` | `DataSource` | Not Stated | TBD | `TBD` | `Data/DataSource.cs` |
| Core Component | `Data` | `DataStoreEntity` | Not Stated | TBD | `TBD` | `Data/DataStoreEntity.cs` |
| Manager Component | `Data` | `DataStoreManager` | Not Stated | TBD | `TBD` | `Data/DataStoreManager.cs` |
| Provider Component | `Data` | `GenericSqlDataProvider` | Not Stated | TBD | `TBD` | `Data/GenericSqlDataProvider.cs` |
| Core Component | `Data` | `GenericSqlDataProviderBase` | Not Stated | TBD | `TBD` | `Data/GenericSqlDataProviderBase.cs` |
| Manager Component | `Data` | `IDataManager` | Not Stated | TBD | `TBD` | `Data/IDataManager.cs` |
| Provider Component | `Data` | `IDataProvider` | Not Stated | TBD | `TBD` | `Data/IDataProvider.cs` |
| Manager Component | `Data` | `IDataSourceManager` | Not Stated | TBD | `TBD` | `Data/IDataSourceManager.cs` |
| Core Component | `Data` | `SqlDataProviderBase` | Not Stated | TBD | `TBD` | `Data/SqlDataProviderBase.cs` |
| Core Component | `Data` | `SqlQueryGenerator` | Not Stated | TBD | `TBD` | `Data/SqlQueryGenerator.cs` |
| Provider Component | `Data` | `VersionSqlDataProvider` | Not Stated | TBD | `TBD` | `Data/VersionSqlDataProvider.cs` |
| Core Component | `Diagnostics` | `EventLog` | Not Stated | TBD | `TBD` | `Diagnostics/EventLog.cs` |
| Provider Component | `Diagnostics` | `IEventLogProvider` | Not Stated | TBD | `TBD` | `Diagnostics/IEventLogProvider.cs` |
| Core Component | `Diagnostics` | `PerformanceLog` | Not Stated | TBD | `TBD` | `Diagnostics/PerformanceLog.cs` |
| Core Component | `IO` | `PathResolver` | Not Stated | TBD | `TBD` | `IO/PathResolver.cs` |
| Core Component | `(root)` | `IPageElement` | Not Stated | TBD | `TBD` | `IPageElement.cs` |
| Core Component | `(root)` | `ISharableContent` | Not Stated | TBD | `TBD` | `ISharableContent.cs` |
| Provider Component | `(root)` | `IWebAttachmentProvider` | Not Stated | TBD | `TBD` | `IWebAttachmentProvider.cs` |
| Provider Component | `(root)` | `IWebCategoryProvider` | Not Stated | TBD | `TBD` | `IWebCategoryProvider.cs` |
| Provider Component | `(root)` | `IWebCommentProvider` | Not Stated | TBD | `TBD` | `IWebCommentProvider.cs` |
| Provider Component | `(root)` | `IWebConstantProvider` | Not Stated | TBD | `TBD` | `IWebConstantProvider.cs` |
| Provider Component | `(root)` | `IWebFileProvider` | Not Stated | TBD | `TBD` | `IWebFileProvider.cs` |
| Provider Component | `(root)` | `IWebFolderProvider` | Not Stated | TBD | `TBD` | `IWebFolderProvider.cs` |
| Provider Component | `(root)` | `IWebJobProvider` | Not Stated | TBD | `TBD` | `IWebJobProvider.cs` |
| Core Component | `(root)` | `IWebObject` | Not Stated | TBD | `TBD` | `IWebObject.cs` |
| Provider Component | `(root)` | `IWebObjectHeaderProvider` | Not Stated | TBD | `TBD` | `IWebObjectHeaderProvider.cs` |
| Provider Component | `(root)` | `IWebObjectIPAddressProvider` | Not Stated | TBD | `TBD` | `IWebObjectIPAddressProvider.cs` |
| Provider Component | `(root)` | `IWebObjectProvider` | Not Stated | TBD | `TBD` | `IWebObjectProvider.cs` |
| Provider Component | `(root)` | `IWebParameterProvider` | Not Stated | TBD | `TBD` | `IWebParameterProvider.cs` |
| Provider Component | `(root)` | `IWebParameterSetProvider` | Not Stated | TBD | `TBD` | `IWebParameterSetProvider.cs` |
| Provider Component | `(root)` | `IWebRegistryProvider` | Not Stated | TBD | `TBD` | `IWebRegistryProvider.cs` |
| Provider Component | `(root)` | `IWebSiteIdentityProvider` | Not Stated | TBD | `TBD` | `IWebSiteIdentityProvider.cs` |
| Provider Component | `(root)` | `IWebSubscriptionProvider` | Not Stated | TBD | `TBD` | `IWebSubscriptionProvider.cs` |
| Provider Component | `(root)` | `IWebTextResourceProvider` | Not Stated | TBD | `TBD` | `IWebTextResourceProvider.cs` |
| Manager Component | `Manager` | `UserProviderManager` | Not Stated | TBD | `TBD` | `Manager/UserProviderManager.cs` |
| Manager Component | `Manager` | `WebConstantManager` | Not Stated | TBD | `TBD` | `Manager/WebConstantManager.cs` |
| Manager Component | `Manager` | `WebGlobalPolicyManager` | Not Stated | TBD | `TBD` | `Manager/WebGlobalPolicyManager.cs` |
| Manager Component | `Manager` | `WebGroupManager` | Not Stated | TBD | `TBD` | `Manager/WebGroupManager.cs` |
| Manager Component | `Manager` | `WebMasterPageManager` | Not Stated | TBD | `TBD` | `Manager/WebMasterPageManager.cs` |
| Manager Component | `Manager` | `WebObjectHeaderManager` | Not Stated | TBD | `TBD` | `Manager/WebObjectHeaderManager.cs` |
| Manager Component | `Manager` | `WebObjectManager` | Not Stated | TBD | `TBD` | `Manager/WebObjectManager.cs` |
| Manager Component | `Manager` | `WebObjectSecurityManager` | Not Stated | TBD | `TBD` | `Manager/WebObjectSecurityManager.cs` |
| Manager Component | `Manager` | `WebObjectSecurityPermissionManager` | Not Stated | TBD | `TBD` | `Manager/WebObjectSecurityPermissionManager.cs` |
| Manager Component | `Manager` | `WebPageElementManager` | Not Stated | TBD | `TBD` | `Manager/WebPageElementManager.cs` |
| Manager Component | `Manager` | `WebPageManager` | Not Stated | TBD | `TBD` | `Manager/WebPageManager.cs` |
| Manager Component | `Manager` | `WebPagePanelManager` | Not Stated | TBD | `TBD` | `Manager/WebPagePanelManager.cs` |
| Manager Component | `Manager` | `WebParameterManager` | Not Stated | TBD | `TBD` | `Manager/WebParameterManager.cs` |
| Manager Component | `Manager` | `WebParameterSetManager` | Not Stated | TBD | `TBD` | `Manager/WebParameterSetManager.cs` |
| Manager Component | `Manager` | `WebPartControlManager` | Not Stated | TBD | `TBD` | `Manager/WebPartControlManager.cs` |
| Manager Component | `Manager` | `WebPartControlTemplateManager` | Not Stated | TBD | `TBD` | `Manager/WebPartControlTemplateManager.cs` |
| Manager Component | `Manager` | `WebPartManager` | Not Stated | TBD | `TBD` | `Manager/WebPartManager.cs` |
| Manager Component | `Manager` | `WebPermissionManager` | Not Stated | TBD | `TBD` | `Manager/WebPermissionManager.cs` |
| Manager Component | `Manager` | `WebRegistryManager` | Not Stated | TBD | `TBD` | `Manager/WebRegistryManager.cs` |
| Manager Component | `Manager` | `WebShareManager` | Not Stated | TBD | `TBD` | `Manager/WebShareManager.cs` |
| Manager Component | `Manager` | `WebShortUrlManager` | Not Stated | TBD | `TBD` | `Manager/WebShortUrlManager.cs` |
| Manager Component | `Manager` | `WebSiteIdentityManager` | Not Stated | TBD | `TBD` | `Manager/WebSiteIdentityManager.cs` |
| Manager Component | `Manager` | `WebSiteManager` | Not Stated | TBD | `TBD` | `Manager/WebSiteManager.cs` |
| Manager Component | `Manager` | `WebSkinManager` | Not Stated | TBD | `TBD` | `Manager/WebSkinManager.cs` |
| Manager Component | `Manager` | `WebSubscriptionManager` | Not Stated | TBD | `TBD` | `Manager/WebSubscriptionManager.cs` |
| Manager Component | `Manager` | `WebTemplateManager` | Not Stated | TBD | `TBD` | `Manager/WebTemplateManager.cs` |
| Manager Component | `Manager` | `WebTemplatePanelManager` | Not Stated | TBD | `TBD` | `Manager/WebTemplatePanelManager.cs` |
| Manager Component | `Manager` | `WebTextResourceManager` | Not Stated | TBD | `TBD` | `Manager/WebTextResourceManager.cs` |
| Manager Component | `Manager` | `WebThemeManager` | Not Stated | TBD | `TBD` | `Manager/WebThemeManager.cs` |
| Manager Component | `Manager` | `WebUserGroupManager` | Not Stated | TBD | `TBD` | `Manager/WebUserGroupManager.cs` |
| Manager Component | `Manager` | `WebUserManager` | Not Stated | TBD | `TBD` | `Manager/WebUserManager.cs` |
| Manager Component | `Net` | `AttachmentManager` | Not Stated | TBD | `TBD` | `Net/AttachmentManager.cs` |
| Core Component | `Net` | `CmsEmail` | Not Stated | TBD | `TBD` | `Net/CmsEmail.cs` |
| Core Component | `Net` | `FileSyncInfo` | Not Stated | TBD | `TBD` | `Net/FileSyncInfo.cs` |
| Provider Component | `Net` | `IWebMessageQueueProvider` | Not Stated | TBD | `TBD` | `Net/IWebMessageQueueProvider.cs` |
| Task/Job Component | `Net` | `MessageProcessorTask` | Not Stated | TBD | `TBD` | `Net/MessageProcessorTask.cs` |
| Core Component | `Net` | `SmsConfig` | Not Stated | TBD | `TBD` | `Net/SmsConfig.cs` |
| Core Component | `Net` | `SmsMessage` | Not Stated | TBD | `TBD` | `Net/SmsMessage.cs` |
| Core Component | `Net` | `WebMailMessage` | Not Stated | TBD | `TBD` | `Net/WebMailMessage.cs` |
| Core Component | `Net` | `WebMessageQueue` | Not Stated | TBD | `TBD` | `Net/WebMessageQueue.cs` |
| Core Component | `(root)` | `ObjectKey` | Not Stated | TBD | `TBD` | `ObjectKey.cs` |
| Core Component | `(root)` | `ObjectRecordPair` | Not Stated | TBD | `TBD` | `ObjectRecordPair.cs` |
| Manager Component | `PartModel` | `IPartDataManager` | Not Stated | TBD | `TBD` | `PartModel/IPartDataManager.cs` |
| Provider Component | `PartModel` | `IWebPartAdminProvider` | Not Stated | TBD | `TBD` | `PartModel/IWebPartAdminProvider.cs` |
| Provider Component | `PartModel` | `IWebPartConfigProvider` | Not Stated | TBD | `TBD` | `PartModel/IWebPartConfigProvider.cs` |
| Provider Component | `PartModel` | `IWebPartControlProvider` | Not Stated | TBD | `TBD` | `PartModel/IWebPartControlProvider.cs` |
| Provider Component | `PartModel` | `IWebPartControlTemplateProvider` | Not Stated | TBD | `TBD` | `PartModel/IWebPartControlTemplateProvider.cs` |
| Provider Component | `PartModel` | `IWebPartProvider` | Not Stated | TBD | `TBD` | `PartModel/IWebPartProvider.cs` |
| Core Component | `PartModel` | `PartDataManagerModel` | Not Stated | TBD | `TBD` | `PartModel/PartDataManagerModel.cs` |
| Core Component | `PartModel` | `WebPart` | Not Stated | TBD | `TBD` | `PartModel/WebPart.cs` |
| Core Component | `PartModel` | `WebPartAdmin` | Not Stated | TBD | `TBD` | `PartModel/WebPartAdmin.cs` |
| Core Component | `PartModel` | `WebPartConfig` | Not Stated | TBD | `TBD` | `PartModel/WebPartConfig.cs` |
| Core Component | `PartModel` | `WebPartControl` | Not Stated | TBD | `TBD` | `PartModel/WebPartControl.cs` |
| Core Component | `PartModel` | `WebPartControlTemplate` | Not Stated | TBD | `TBD` | `PartModel/WebPartControlTemplate.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Not Stated | TBD | `TBD` | `Properties/AssemblyInfo.cs` |
| Core Component | `Security` | `IPublicSecurable` | Not Stated | TBD | `TBD` | `Security/IPublicSecurable.cs` |
| Provider Component | `Security` | `IUserProvider` | Not Stated | TBD | `TBD` | `Security/IUserProvider.cs` |
| Provider Component | `Security` | `IUserProviderProvider` | Not Stated | TBD | `TBD` | `Security/IUserProviderProvider.cs` |
| Core Component | `Security` | `IWebAccount` | Not Stated | TBD | `TBD` | `Security/IWebAccount.cs` |
| Provider Component | `Security` | `IWebAddressProvider` | Not Stated | TBD | `TBD` | `Security/IWebAddressProvider.cs` |
| Provider Component | `Security` | `IWebGlobalPolicyProvider` | Not Stated | TBD | `TBD` | `Security/IWebGlobalPolicyProvider.cs` |
| Provider Component | `Security` | `IWebGroupProvider` | Not Stated | TBD | `TBD` | `Security/IWebGroupProvider.cs` |
| Provider Component | `Security` | `IWebObjectSecurityPermissionProvider` | Not Stated | TBD | `TBD` | `Security/IWebObjectSecurityPermissionProvider.cs` |
| Provider Component | `Security` | `IWebObjectSecurityProvider` | Not Stated | TBD | `TBD` | `Security/IWebObjectSecurityProvider.cs` |
| Provider Component | `Security` | `IWebPermissionProvider` | Not Stated | TBD | `TBD` | `Security/IWebPermissionProvider.cs` |
| Provider Component | `Security` | `IWebPermissionSetProvider` | Not Stated | TBD | `TBD` | `Security/IWebPermissionSetProvider.cs` |
| Provider Component | `Security` | `IWebRoleProvider` | Not Stated | TBD | `TBD` | `Security/IWebRoleProvider.cs` |
| Provider Component | `Security` | `IWebShareProvider` | Not Stated | TBD | `TBD` | `Security/IWebShareProvider.cs` |
| Provider Component | `Security` | `IWebUserGroupProvider` | Not Stated | TBD | `TBD` | `Security/IWebUserGroupProvider.cs` |
| Provider Component | `Security` | `IWebUserProvider` | Not Stated | TBD | `TBD` | `Security/IWebUserProvider.cs` |
| Provider Component | `Security` | `IWebUserRoleProvider` | Not Stated | TBD | `TBD` | `Security/IWebUserRoleProvider.cs` |
| Manager Component | `Security` | `LoginCookieManager` | Not Stated | TBD | `TBD` | `Security/LoginCookieManager.cs` |
| Core Component | `Security` | `OtpCache` | Not Stated | TBD | `TBD` | `Security/OtpCache.cs` |
| Core Component | `Security` | `OtpCodeGenerator` | Not Stated | TBD | `TBD` | `Security/OtpCodeGenerator.cs` |
| Core Component | `Security` | `PublicSecurableObject` | Not Stated | TBD | `TBD` | `Security/PublicSecurableObject.cs` |
| Core Component | `Security` | `SecurableObject` | Not Stated | TBD | `TBD` | `Security/SecurableObject.cs` |
| Core Component | `Security` | `UserInfo` | Not Stated | TBD | `TBD` | `Security/UserInfo.cs` |
| Provider Component | `Security` | `UserProvider` | Not Stated | TBD | `TBD` | `Security/UserProvider.cs` |
| Core Component | `Security` | `UserSession` | Not Stated | TBD | `TBD` | `Security/UserSession.cs` |
| Core Component | `Security` | `UserSessionBrowser` | Not Stated | TBD | `TBD` | `Security/UserSessionBrowser.cs` |
| Manager Component | `Security` | `UserSessionManager` | Not Stated | TBD | `TBD` | `Security/UserSessionManager.cs` |
| Core Component | `Security` | `WSUserInfo` | Not Stated | TBD | `TBD` | `Security/WSUserInfo.cs` |
| Core Component | `Security` | `WebAddress` | Not Stated | TBD | `TBD` | `Security/WebAddress.cs` |
| Core Component | `Security` | `WebGlobalPolicy` | Not Stated | TBD | `TBD` | `Security/WebGlobalPolicy.cs` |
| Core Component | `Security` | `WebGroup` | Not Stated | TBD | `TBD` | `Security/WebGroup.cs` |
| Core Component | `Security` | `WebPermission` | Not Stated | TBD | `TBD` | `Security/WebPermission.cs` |
| Core Component | `Security` | `WebPermissionSet` | Not Stated | TBD | `TBD` | `Security/WebPermissionSet.cs` |
| Core Component | `Security` | `WebRole` | Not Stated | TBD | `TBD` | `Security/WebRole.cs` |
| Core Component | `Security` | `WebShare` | Not Stated | TBD | `TBD` | `Security/WebShare.cs` |
| Core Component | `Security` | `WebUser` | Not Stated | TBD | `TBD` | `Security/WebUser.cs` |
| Core Component | `Security` | `WebUserGroup` | Not Stated | TBD | `TBD` | `Security/WebUserGroup.cs` |
| Provider Component | `SiteModel` | `IWebMasterPageProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebMasterPageProvider.cs` |
| Provider Component | `SiteModel` | `IWebPageElementProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebPageElementProvider.cs` |
| Provider Component | `SiteModel` | `IWebPagePanelProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebPagePanelProvider.cs` |
| Provider Component | `SiteModel` | `IWebPageProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebPageProvider.cs` |
| Provider Component | `SiteModel` | `IWebShortUrlProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebShortUrlProvider.cs` |
| Provider Component | `SiteModel` | `IWebSiteProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebSiteProvider.cs` |
| Provider Component | `SiteModel` | `IWebSkinProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebSkinProvider.cs` |
| Provider Component | `SiteModel` | `IWebTemplatePanelProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebTemplatePanelProvider.cs` |
| Provider Component | `SiteModel` | `IWebTemplateProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebTemplateProvider.cs` |
| Provider Component | `SiteModel` | `IWebThemeProvider` | Not Stated | TBD | `TBD` | `SiteModel/IWebThemeProvider.cs` |
| Core Component | `SiteModel` | `WebContextBase` | Not Stated | TBD | `TBD` | `SiteModel/WebContextBase.cs` |
| Core Component | `SiteModel` | `WebMasterPage` | Not Stated | TBD | `TBD` | `SiteModel/WebMasterPage.cs` |
| Core Component | `SiteModel` | `WebObjectItem` | Not Stated | TBD | `TBD` | `SiteModel/WebObjectItem.cs` |
| Core Component | `SiteModel` | `WebPage` | Not Stated | TBD | `TBD` | `SiteModel/WebPage.cs` |
| Core Component | `SiteModel` | `WebPageElement` | Not Stated | TBD | `TBD` | `SiteModel/WebPageElement.cs` |
| Core Component | `SiteModel` | `WebPagePanel` | Not Stated | TBD | `TBD` | `SiteModel/WebPagePanel.cs` |
| Core Component | `SiteModel` | `WebShortUrl` | Not Stated | TBD | `TBD` | `SiteModel/WebShortUrl.cs` |
| Core Component | `SiteModel` | `WebSite` | Not Stated | TBD | `TBD` | `SiteModel/WebSite.cs` |
| Core Component | `SiteModel` | `WebSiteIdentity` | Not Stated | TBD | `TBD` | `SiteModel/WebSiteIdentity.cs` |
| Core Component | `SiteModel` | `WebSkin` | Not Stated | TBD | `TBD` | `SiteModel/WebSkin.cs` |
| Core Component | `SiteModel` | `WebTemplate` | Not Stated | TBD | `TBD` | `SiteModel/WebTemplate.cs` |
| Core Component | `SiteModel` | `WebTemplatePanel` | Not Stated | TBD | `TBD` | `SiteModel/WebTemplatePanel.cs` |
| Core Component | `SiteModel` | `WebTheme` | Not Stated | TBD | `TBD` | `SiteModel/WebTheme.cs` |
| Helper Component | `Utilities` | `AccountHelper` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. | `Utilities/AccountHelper.cs` |
| Helper Component | `Utilities` | `HtmlHelper` | Not Stated | TBD | `TBD` | `Utilities/HtmlHelper.cs` |
| Core Component | `Utilities` | `LoginSecurity` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. | `Utilities/LoginSecurity.cs` |
| Helper Component | `Utilities` | `ParameterHelper` | Not Stated | TBD | `TBD` | `Utilities/ParameterHelper.cs` |
| Helper Component | `Utilities` | `SecurityHelper` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. | `Utilities/SecurityHelper.cs` |
| Helper Component | `Utilities` | `SyncHelper` | Not Stated | TBD | `TBD` | `Utilities/SyncHelper.cs` |
| Core Component | `Utilities` | `UserDataTag` | Not Stated | TBD | `TBD` | `Utilities/UserDataTag.cs` |
| Core Component | `Utilities` | `UserIdEqualityComparer` | Not Stated | TBD | `TBD` | `Utilities/UserIdEqualityComparer.cs` |
| Core Component | `Utilities` | `UserNameEqualityComparer` | Not Stated | TBD | `TBD` | `Utilities/UserNameEqualityComparer.cs` |
| Core Component | `Utilities` | `UserTagEqualityComparer` | Not Stated | TBD | `TBD` | `Utilities/UserTagEqualityComparer.cs` |
| Helper Component | `Utilities` | `WHelper` | Not Stated | TBD | `TBD` | `Utilities/WHelper.cs` |
| Core Component | `Utilities` | `WebCryptography` | Not Stated | TBD | `TBD` | `Utilities/WebCryptography.cs` |
| Core Component | `Utilities` | `WebRedirector` | Not Stated | TBD | `TBD` | `Utilities/WebRedirector.cs` |
| Core Component | `Utilities` | `WebSetup` | Not Stated | TBD | `TBD` | `Utilities/WebSetup.cs` |
| Core Component | `Utilities` | `WebSubstituter` | Not Stated | TBD | `TBD` | `Utilities/WebSubstituter.cs` |
| Core Component | `(root)` | `WApproval` | Not Stated | TBD | `TBD` | `WApproval.cs` |
| Core Component | `(root)` | `WApprovalPartial` | Not Stated | TBD | `TBD` | `WApprovalPartial.cs` |
| Core Component | `(root)` | `WConfig` | Not Stated | TBD | `TBD` | `WConfig.cs` |
| Core Component | `(root)` | `WContext` | Not Stated | TBD | `TBD` | `WContext.cs` |
| Core Component | `(root)` | `WFrameworkModel.Context` | Not Stated | TBD | `TBD` | `WFrameworkModel.Context.cs` |
| Core Component | `(root)` | `WFrameworkModel` | Not Stated | TBD | `TBD` | `WFrameworkModel.cs` |
| Core Component | `(root)` | `WQuery` | Not Stated | TBD | `TBD` | `WQuery.cs` |
| Core Component | `(root)` | `WSession` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. | `WSession.cs` |
| Core Component | `(root)` | `WebCategory` | Not Stated | TBD | `TBD` | `WebCategory.cs` |
| Core Component | `(root)` | `WebRewriter` | Not Stated | TBD | `TBD` | `WebRewriter.cs` |
| Core Component | `(root)` | `WebUserContainer` | Not Stated | TBD | `TBD` | `WebUserContainer.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `App` | Not Stated | TBD | `TBD` | `App.Config` |
| Configuration/Resource | `(root)` | `packages` | Not Stated | TBD | `TBD` | `packages.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

