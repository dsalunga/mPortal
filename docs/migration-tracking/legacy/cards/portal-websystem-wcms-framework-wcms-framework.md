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

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Generated UI Partial | `(root)` | `WFrameworkModel.Designer` | `WFrameworkModel.Designer.cs` | Not Stated | TBD | `TBD` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Core Component | `Agent` | `AgentTaskBase` | `Agent/AgentTaskBase.cs` | Blocked | Replace with durable job orchestration and safe cancellation semantics. | Legacy thread/service execution model; requires durable job orchestration redesign. |
| Core Component | `Agent` | `FrameworkAgent` | `Agent/FrameworkAgent.cs` | Blocked | Replace with durable job orchestration and safe cancellation semantics. | Legacy thread/service execution model; requires durable job orchestration redesign. |
| Task/Job Component | `Agent` | `ITask` | `Agent/ITask.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Agent` | `ITaskRequest` | `Agent/ITaskRequest.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Caching` | `DataSetCache` | `Caching/DataSetCache.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Caching` | `ICacheManager` | `Caching/ICacheManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Caching` | `ICacheable` | `Caching/ICacheable.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Caching` | `MemoryCache` | `Caching/MemoryCache.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Caching` | `NullCache` | `Caching/NullCache.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `Constants` | `Constants.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `INameWebObject` | `Core/INameWebObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `IQueryFilterElement` | `Core/IQueryFilterElement.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Core` | `ISelfManager` | `Core/ISelfManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Core` | `IVersionDataManager` | `Core/IVersionDataManager.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Core` | `IVersionDataProvider` | `Core/IVersionDataProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `IVersionWebObject` | `Core/IVersionWebObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `IWebHeaderTarget` | `Core/IWebHeaderTarget.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `LinkedPart` | `Core/LinkedPart.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `ModelBase` | `Core/ModelBase.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `NamedWebObject` | `Core/NamedWebObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `ObjectCapsule` | `Core/ObjectCapsule.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `ObjectColumnAttribute` | `Core/ObjectColumnAttribute.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Core` | `ObjectManager` | `Core/ObjectManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `PageElementBase` | `Core/PageElementBase.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `ParameterizedWebObject` | `Core/ParameterizedWebObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `QueryFilter` | `Core/QueryFilter.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `QueryFilterElement` | `Core/QueryFilterElement.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core/Shared` | `Country` | `Core/Shared/Country.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Core/Shared` | `CountryProvider` | `Core/Shared/CountryProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core/Shared` | `CountryState` | `Core/Shared/CountryState.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Core/Shared` | `CountryStateProvider` | `Core/Shared/CountryStateProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Core/Shared` | `ICountryProvider` | `Core/Shared/ICountryProvider.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Core` | `StandardDataManager` | `Core/StandardDataManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Core` | `StandardVersionDataManager` | `Core/StandardVersionDataManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebAttachment` | `Core/WebAttachment.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebComment` | `Core/WebComment.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebConstant` | `Core/WebConstant.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebDirectoryEntry` | `Core/WebDirectoryEntry.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebFile` | `Core/WebFile.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebFolder` | `Core/WebFolder.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebJob` | `Core/WebJob.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObject` | `Core/WebObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectAttribute` | `Core/WebObjectAttribute.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectBase` | `Core/WebObjectBase.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectHeader` | `Core/WebObjectHeader.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectIPAddress` | `Core/WebObjectIPAddress.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectLink` | `Core/WebObjectLink.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectSecurity` | `Core/WebObjectSecurity.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebObjectSecurityPermission` | `Core/WebObjectSecurityPermission.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebOffice` | `Core/WebOffice.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebParameter` | `Core/WebParameter.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebParameterSet` | `Core/WebParameterSet.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebRegistry` | `Core/WebRegistry.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebSubscription` | `Core/WebSubscription.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Core` | `WebTextResource` | `Core/WebTextResource.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Core` | `XmlDataProvider` | `Core/XmlDataProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Data` | `DataAccess` | `Data/DataAccess.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Data` | `DataSource` | `Data/DataSource.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Data` | `DataStoreEntity` | `Data/DataStoreEntity.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Data` | `DataStoreManager` | `Data/DataStoreManager.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Data` | `GenericSqlDataProvider` | `Data/GenericSqlDataProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Data` | `GenericSqlDataProviderBase` | `Data/GenericSqlDataProviderBase.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Data` | `IDataManager` | `Data/IDataManager.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Data` | `IDataProvider` | `Data/IDataProvider.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Data` | `IDataSourceManager` | `Data/IDataSourceManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Data` | `SqlDataProviderBase` | `Data/SqlDataProviderBase.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Data` | `SqlQueryGenerator` | `Data/SqlQueryGenerator.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Data` | `VersionSqlDataProvider` | `Data/VersionSqlDataProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Diagnostics` | `EventLog` | `Diagnostics/EventLog.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Diagnostics` | `IEventLogProvider` | `Diagnostics/IEventLogProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Diagnostics` | `PerformanceLog` | `Diagnostics/PerformanceLog.cs` | Not Stated | TBD | `TBD` |
| Core Component | `IO` | `PathResolver` | `IO/PathResolver.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `IPageElement` | `IPageElement.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `ISharableContent` | `ISharableContent.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebAttachmentProvider` | `IWebAttachmentProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebCategoryProvider` | `IWebCategoryProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebCommentProvider` | `IWebCommentProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebConstantProvider` | `IWebConstantProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebFileProvider` | `IWebFileProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebFolderProvider` | `IWebFolderProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebJobProvider` | `IWebJobProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `IWebObject` | `IWebObject.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebObjectHeaderProvider` | `IWebObjectHeaderProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebObjectIPAddressProvider` | `IWebObjectIPAddressProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebObjectProvider` | `IWebObjectProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebParameterProvider` | `IWebParameterProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebParameterSetProvider` | `IWebParameterSetProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebRegistryProvider` | `IWebRegistryProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebSiteIdentityProvider` | `IWebSiteIdentityProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebSubscriptionProvider` | `IWebSubscriptionProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `(root)` | `IWebTextResourceProvider` | `IWebTextResourceProvider.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `UserProviderManager` | `Manager/UserProviderManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebConstantManager` | `Manager/WebConstantManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebGlobalPolicyManager` | `Manager/WebGlobalPolicyManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebGroupManager` | `Manager/WebGroupManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebMasterPageManager` | `Manager/WebMasterPageManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebObjectHeaderManager` | `Manager/WebObjectHeaderManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebObjectManager` | `Manager/WebObjectManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebObjectSecurityManager` | `Manager/WebObjectSecurityManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebObjectSecurityPermissionManager` | `Manager/WebObjectSecurityPermissionManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPageElementManager` | `Manager/WebPageElementManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPageManager` | `Manager/WebPageManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPagePanelManager` | `Manager/WebPagePanelManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebParameterManager` | `Manager/WebParameterManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebParameterSetManager` | `Manager/WebParameterSetManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPartControlManager` | `Manager/WebPartControlManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPartControlTemplateManager` | `Manager/WebPartControlTemplateManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPartManager` | `Manager/WebPartManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebPermissionManager` | `Manager/WebPermissionManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebRegistryManager` | `Manager/WebRegistryManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebShareManager` | `Manager/WebShareManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebShortUrlManager` | `Manager/WebShortUrlManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebSiteIdentityManager` | `Manager/WebSiteIdentityManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebSiteManager` | `Manager/WebSiteManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebSkinManager` | `Manager/WebSkinManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebSubscriptionManager` | `Manager/WebSubscriptionManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebTemplateManager` | `Manager/WebTemplateManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebTemplatePanelManager` | `Manager/WebTemplatePanelManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebTextResourceManager` | `Manager/WebTextResourceManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebThemeManager` | `Manager/WebThemeManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebUserGroupManager` | `Manager/WebUserGroupManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Manager` | `WebUserManager` | `Manager/WebUserManager.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Net` | `AttachmentManager` | `Net/AttachmentManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Net` | `CmsEmail` | `Net/CmsEmail.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Net` | `FileSyncInfo` | `Net/FileSyncInfo.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Net` | `IWebMessageQueueProvider` | `Net/IWebMessageQueueProvider.cs` | Not Stated | TBD | `TBD` |
| Task/Job Component | `Net` | `MessageProcessorTask` | `Net/MessageProcessorTask.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Net` | `SmsConfig` | `Net/SmsConfig.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Net` | `SmsMessage` | `Net/SmsMessage.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Net` | `WebMailMessage` | `Net/WebMailMessage.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Net` | `WebMessageQueue` | `Net/WebMessageQueue.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `ObjectKey` | `ObjectKey.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `ObjectRecordPair` | `ObjectRecordPair.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `PartModel` | `IPartDataManager` | `PartModel/IPartDataManager.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `PartModel` | `IWebPartAdminProvider` | `PartModel/IWebPartAdminProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `PartModel` | `IWebPartConfigProvider` | `PartModel/IWebPartConfigProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `PartModel` | `IWebPartControlProvider` | `PartModel/IWebPartControlProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `PartModel` | `IWebPartControlTemplateProvider` | `PartModel/IWebPartControlTemplateProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `PartModel` | `IWebPartProvider` | `PartModel/IWebPartProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `PartModel` | `PartDataManagerModel` | `PartModel/PartDataManagerModel.cs` | Not Stated | TBD | `TBD` |
| Core Component | `PartModel` | `WebPart` | `PartModel/WebPart.cs` | Not Stated | TBD | `TBD` |
| Core Component | `PartModel` | `WebPartAdmin` | `PartModel/WebPartAdmin.cs` | Not Stated | TBD | `TBD` |
| Core Component | `PartModel` | `WebPartConfig` | `PartModel/WebPartConfig.cs` | Not Stated | TBD | `TBD` |
| Core Component | `PartModel` | `WebPartControl` | `PartModel/WebPartControl.cs` | Not Stated | TBD | `TBD` |
| Core Component | `PartModel` | `WebPartControlTemplate` | `PartModel/WebPartControlTemplate.cs` | Not Stated | TBD | `TBD` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `IPublicSecurable` | `Security/IPublicSecurable.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IUserProvider` | `Security/IUserProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IUserProviderProvider` | `Security/IUserProviderProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `IWebAccount` | `Security/IWebAccount.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebAddressProvider` | `Security/IWebAddressProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebGlobalPolicyProvider` | `Security/IWebGlobalPolicyProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebGroupProvider` | `Security/IWebGroupProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebObjectSecurityPermissionProvider` | `Security/IWebObjectSecurityPermissionProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebObjectSecurityProvider` | `Security/IWebObjectSecurityProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebPermissionProvider` | `Security/IWebPermissionProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebPermissionSetProvider` | `Security/IWebPermissionSetProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebRoleProvider` | `Security/IWebRoleProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebShareProvider` | `Security/IWebShareProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebUserGroupProvider` | `Security/IWebUserGroupProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebUserProvider` | `Security/IWebUserProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `IWebUserRoleProvider` | `Security/IWebUserRoleProvider.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Security` | `LoginCookieManager` | `Security/LoginCookieManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `OtpCache` | `Security/OtpCache.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `OtpCodeGenerator` | `Security/OtpCodeGenerator.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `PublicSecurableObject` | `Security/PublicSecurableObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `SecurableObject` | `Security/SecurableObject.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `UserInfo` | `Security/UserInfo.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `Security` | `UserProvider` | `Security/UserProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `UserSession` | `Security/UserSession.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `UserSessionBrowser` | `Security/UserSessionBrowser.cs` | Not Stated | TBD | `TBD` |
| Manager Component | `Security` | `UserSessionManager` | `Security/UserSessionManager.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WSUserInfo` | `Security/WSUserInfo.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebAddress` | `Security/WebAddress.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebGlobalPolicy` | `Security/WebGlobalPolicy.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebGroup` | `Security/WebGroup.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebPermission` | `Security/WebPermission.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebPermissionSet` | `Security/WebPermissionSet.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebRole` | `Security/WebRole.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebShare` | `Security/WebShare.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebUser` | `Security/WebUser.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Security` | `WebUserGroup` | `Security/WebUserGroup.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebMasterPageProvider` | `SiteModel/IWebMasterPageProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebPageElementProvider` | `SiteModel/IWebPageElementProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebPagePanelProvider` | `SiteModel/IWebPagePanelProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebPageProvider` | `SiteModel/IWebPageProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebShortUrlProvider` | `SiteModel/IWebShortUrlProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebSiteProvider` | `SiteModel/IWebSiteProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebSkinProvider` | `SiteModel/IWebSkinProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebTemplatePanelProvider` | `SiteModel/IWebTemplatePanelProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebTemplateProvider` | `SiteModel/IWebTemplateProvider.cs` | Not Stated | TBD | `TBD` |
| Provider Component | `SiteModel` | `IWebThemeProvider` | `SiteModel/IWebThemeProvider.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebContextBase` | `SiteModel/WebContextBase.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebMasterPage` | `SiteModel/WebMasterPage.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebObjectItem` | `SiteModel/WebObjectItem.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebPage` | `SiteModel/WebPage.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebPageElement` | `SiteModel/WebPageElement.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebPagePanel` | `SiteModel/WebPagePanel.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebShortUrl` | `SiteModel/WebShortUrl.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebSite` | `SiteModel/WebSite.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebSiteIdentity` | `SiteModel/WebSiteIdentity.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebSkin` | `SiteModel/WebSkin.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebTemplate` | `SiteModel/WebTemplate.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebTemplatePanel` | `SiteModel/WebTemplatePanel.cs` | Not Stated | TBD | `TBD` |
| Core Component | `SiteModel` | `WebTheme` | `SiteModel/WebTheme.cs` | Not Stated | TBD | `TBD` |
| Helper Component | `Utilities` | `AccountHelper` | `Utilities/AccountHelper.cs` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. |
| Helper Component | `Utilities` | `HtmlHelper` | `Utilities/HtmlHelper.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `LoginSecurity` | `Utilities/LoginSecurity.cs` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. |
| Helper Component | `Utilities` | `ParameterHelper` | `Utilities/ParameterHelper.cs` | Not Stated | TBD | `TBD` |
| Helper Component | `Utilities` | `SecurityHelper` | `Utilities/SecurityHelper.cs` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. |
| Helper Component | `Utilities` | `SyncHelper` | `Utilities/SyncHelper.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `UserDataTag` | `Utilities/UserDataTag.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `UserIdEqualityComparer` | `Utilities/UserIdEqualityComparer.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `UserNameEqualityComparer` | `Utilities/UserNameEqualityComparer.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `UserTagEqualityComparer` | `Utilities/UserTagEqualityComparer.cs` | Not Stated | TBD | `TBD` |
| Helper Component | `Utilities` | `WHelper` | `Utilities/WHelper.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `WebCryptography` | `Utilities/WebCryptography.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `WebRedirector` | `Utilities/WebRedirector.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `WebSetup` | `Utilities/WebSetup.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Utilities` | `WebSubstituter` | `Utilities/WebSubstituter.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WApproval` | `WApproval.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WApprovalPartial` | `WApprovalPartial.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WConfig` | `WConfig.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WContext` | `WContext.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WFrameworkModel.Context` | `WFrameworkModel.Context.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WFrameworkModel` | `WFrameworkModel.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WQuery` | `WQuery.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WSession` | `WSession.cs` | Blocked | Rebuild using modern identity/session architecture and managed secrets. | Legacy security/auth/session pattern; migrate via modern identity and secrets architecture. |
| Core Component | `(root)` | `WebCategory` | `WebCategory.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WebRewriter` | `WebRewriter.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `WebUserContainer` | `WebUserContainer.cs` | Not Stated | TBD | `TBD` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `App` | `App.Config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `packages` | `packages.config` | Not Stated | TBD | `TBD` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

