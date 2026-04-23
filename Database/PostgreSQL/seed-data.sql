-- =============================================================================
-- mPortal CMS - PostgreSQL Runtime Baseline Seed
--
-- This script is idempotent and creates the minimum CMS baseline required for:
-- - site/page resolution
-- - CMS fallback rendering
-- - framework object/provider metadata initialization
--
-- NOTE:
-- - Test-only fixture records are defined in seed-test-fixtures.sql.
-- =============================================================================

BEGIN;

-- -----------------------------------------------------------------------------
-- 1) WebObject metadata (provider/type registry)
-- -----------------------------------------------------------------------------
INSERT INTO "WebObject" (
    "Id", "Name", "IdentityColumn", "ObjectType", "Owner", "Prefix",
    "LastRecordId", "MaxCacheCount", "AccessTypeId", "CacheTypeId", "MaxHistoryCount",
    "DataProviderName", "TypeName", "ManagerName", "CacheInterval", "DateModified"
)
VALUES
(1,  'WebConstant',            'ConstantId',        'T', 'WebConstant',            '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebConstantProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebConstantManager, WCMS.Framework', -1, NOW()),
(3,  'WebPage',                'PageId',            'T', 'WebPage',                '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebPageProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WPage, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebPageManager, WCMS.Framework', -1, NOW()),
(4,  'WebSite',                'SiteId',            'T', 'WebSite',                '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebSiteProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WSite, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebSiteManager, WCMS.Framework', -1, NOW()),
(5,  'WebMasterPage',          'MasterPageId',      'T', 'WebMasterPage',          '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebMasterPageProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebMasterPage, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebMasterPageManager, WCMS.Framework', -1, NOW()),
(6,  'WebContent',             'ContentId',         'T', 'WebContent',             '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(7,  'WebObjectContent',       'Id',                'T', 'WebObjectContent',       '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(8,  'WebObjectHeader',        'Id',                'T', 'WebObjectHeader',        '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(9,  'WebObject',              'Id',                'T', 'WebObject',              '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebObjectProvider, WCMS.Framework.Core.SqlProvider', '', '', -1, NOW()),
(10, 'WebPartAdmin',           'PartAdminId',       'T', 'WebPartAdmin',           '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(11, 'WebPartConfig',          'ConfigId',          'T', 'WebPartConfig',          '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(12, 'WebPartControl',         'ControlId',         'T', 'WebPartControl',         '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebPartControlProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebPartControlManager, WCMS.Framework', -1, NOW()),
(13, 'WebPartControlTemplate', 'PartControlTemplateId', 'T', 'WebPartControlTemplate', '', 0, -1, -1, 1, -1, 'WCMS.Framework.Core.SqlProvider.WebPartControlTemplateProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebPartControlTemplateManager, WCMS.Framework', -1, NOW()),
(14, 'WebPart',                'PartId',            'T', 'WebPart',                '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebPartProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebPartManager, WCMS.Framework', -1, NOW()),
(15, 'WebRegistry',            'RegistryId',        'T', 'WebRegistry',            '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebRegistryProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.Core.WebRegistry, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebRegistryManager, WCMS.Framework', -1, NOW()),
(16, 'WebSiteIdentity',        'Id',                'T', 'WebSiteIdentity',        '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebSiteIdentityProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.Core.WebSiteIdentity, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebSiteIdentityManager, WCMS.Framework', -1, NOW()),
(18, 'WebTemplatePanel',       'TemplatePanelId',   'T', 'WebTemplatePanel',       '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebTemplatePanelProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebTemplatePanel, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebTemplatePanelManager, WCMS.Framework', -1, NOW()),
(19, 'WebTemplate',            'Id',                'T', 'WebTemplate',            '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebTemplateProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebTemplate, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebTemplateManager, WCMS.Framework', -1, NOW()),
(20, 'WebTextResource',        'Id',                'T', 'WebTextResource',        '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(21, 'WebUser',                'UserId',            'T', 'WebUser',                '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebUserProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebUser, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebUserManager, WCMS.Framework', -1, NOW()),
(22, 'WebPageElement',         'PageElementId',     'T', 'WebPageElement',         '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebPageElementProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebPageElement, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebPageElementManager, WCMS.Framework', -1, NOW()),
(23, 'WebFolder',              'Id',                'T', 'WebFolder',              '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(25, 'WebRole',                'RoleId',            'T', 'WebRole',                '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(26, 'WebPermission',          'PermissionId',      'T', 'WebPermission',          '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebPermissionProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebPermissionManager, WCMS.Framework', -1, NOW()),
(27, 'WebUserGroup',           'Id',                'T', 'WebUserGroup',           '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(35, 'WebPagePanel',           'PagePanelId',       'T', 'WebPagePanel',           '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebPagePanelProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebPagePanel, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebPagePanelManager, WCMS.Framework', -1, NOW()),
(37, 'WebObjectSecurity',      'Id',                'T', 'WebObjectSecurity',      '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebObjectSecurityProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebObjectSecurityManager, WCMS.Framework', -1, NOW()),
(38, 'WebGroup',               'GroupId',           'T', 'WebGroup',               '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(47, 'WebGlobalPolicy',        'Id',                'T', 'WebGlobalPolicy',        '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebGlobalPolicyProvider, WCMS.Framework.Core.SqlProvider', '', 'WCMS.Framework.Core.Manager.WebGlobalPolicyManager, WCMS.Framework', -1, NOW()),
(50, 'WebPermissionSet',       'Id',                'T', 'WebPermissionSet',       '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(88, 'WebFile',                'Id',                'T', 'WebFile',                '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(90, 'WebTheme',               'Id',                'T', 'WebTheme',               '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebThemeProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebTheme, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebThemeManager, WCMS.Framework', -1, NOW()),
(91, 'WebSkin',                'Id',                'T', 'WebSkin',                '', 0, -1, -1,  1, -1, 'WCMS.Framework.Core.SqlProvider.WebSkinProvider, WCMS.Framework.Core.SqlProvider', 'WCMS.Framework.WebSkin, WCMS.Framework', 'WCMS.Framework.Core.Manager.WebSkinManager, WCMS.Framework', -1, NOW()),
(95, 'WebParameterSet',        'Id',                'T', 'WebParameterSet',        '', 0, -1, -1,  1, -1, '', '', '', -1, NOW()),
(104,'WebAddress',             'Id',                'T', 'WebAddress',             '', 0, -1, -1,  1, -1, '', '', '', -1, NOW())
ON CONFLICT ("Id") DO UPDATE SET
    "Name" = EXCLUDED."Name",
    "IdentityColumn" = EXCLUDED."IdentityColumn",
    "ObjectType" = EXCLUDED."ObjectType",
    "Owner" = EXCLUDED."Owner",
    "Prefix" = EXCLUDED."Prefix",
    "LastRecordId" = EXCLUDED."LastRecordId",
    "MaxCacheCount" = EXCLUDED."MaxCacheCount",
    "AccessTypeId" = EXCLUDED."AccessTypeId",
    "CacheTypeId" = EXCLUDED."CacheTypeId",
    "MaxHistoryCount" = EXCLUDED."MaxHistoryCount",
    "DataProviderName" = EXCLUDED."DataProviderName",
    "TypeName" = EXCLUDED."TypeName",
    "ManagerName" = EXCLUDED."ManagerName",
    "CacheInterval" = EXCLUDED."CacheInterval",
    "DateModified" = EXCLUDED."DateModified";

-- -----------------------------------------------------------------------------
-- 2) System registry baseline
-- -----------------------------------------------------------------------------
INSERT INTO "WebRegistry" ("RegistryId", "Key", "Value", "ParentId", "StageId") VALUES
(1, 'System', NULL, -1, -1),
(2, 'Name', 'mPortal', 1, -1),
(3, 'DefaultSite', '1', 1, -1),
(4, 'DefaultLoginPage', '/login', 1, -1),
(5, 'Debugging', NULL, 1, -1),
(6, 'EnableLogging', '0', 5, -1),
(7, 'AutoLogin', '0', 5, -1),
(8, 'BaseAddress', 'http://localhost:5000', 1, -1),
(9, 'TempFolder', '/tmp/mPortal', 1, -1)
ON CONFLICT ("RegistryId") DO UPDATE SET
    "Key" = EXCLUDED."Key",
    "Value" = EXCLUDED."Value",
    "ParentId" = EXCLUDED."ParentId",
    "StageId" = EXCLUDED."StageId";

-- -----------------------------------------------------------------------------
-- 3) Theme/template/panel/site/page baseline
-- -----------------------------------------------------------------------------
INSERT INTO "WebTemplate" ("Id", "Name", "FileName", "Identity", "PrimaryPanelId", "Version", "VersionOf", "Content", "DateModified", "ThemeId", "Standalone", "ParentId", "SkinId", "TemplateEngineId")
VALUES (1, 'Default Template', 'template.cshtml', 'demo-razor', 1, 1, 1, '', NOW(), -1, 0, -1, -1, 1)
ON CONFLICT ("Id") DO UPDATE SET
    "Name" = EXCLUDED."Name",
    "FileName" = EXCLUDED."FileName",
    "Identity" = EXCLUDED."Identity",
    "PrimaryPanelId" = EXCLUDED."PrimaryPanelId",
    "Version" = EXCLUDED."Version",
    "VersionOf" = EXCLUDED."VersionOf",
    "Content" = EXCLUDED."Content",
    "DateModified" = EXCLUDED."DateModified",
    "ThemeId" = EXCLUDED."ThemeId",
    "Standalone" = EXCLUDED."Standalone",
    "ParentId" = EXCLUDED."ParentId",
    "SkinId" = EXCLUDED."SkinId",
    "TemplateEngineId" = EXCLUDED."TemplateEngineId";

INSERT INTO "WebTemplatePanel" ("TemplatePanelId", "Name", "TemplateId", "PanelName", "Rank", "ObjectId", "RecordId") VALUES
(1, 'Header', 1, 'phHeader', 1, 19, 1),
(2, 'Main', 1, 'phMain', 2, 19, 1),
(3, 'Footer', 1, 'phFooter', 3, 19, 1)
ON CONFLICT ("TemplatePanelId") DO UPDATE SET
    "Name" = EXCLUDED."Name",
    "TemplateId" = EXCLUDED."TemplateId",
    "PanelName" = EXCLUDED."PanelName",
    "Rank" = EXCLUDED."Rank",
    "ObjectId" = EXCLUDED."ObjectId",
    "RecordId" = EXCLUDED."RecordId";

INSERT INTO "WebTheme" ("Id", "TemplateId", "Name", "ParentId", "Identity", "SkinId")
VALUES (1, 1, 'Demo Razor Theme', -1, 'demo-razor', -1)
ON CONFLICT ("Id") DO UPDATE SET
    "TemplateId" = EXCLUDED."TemplateId",
    "Name" = EXCLUDED."Name",
    "ParentId" = EXCLUDED."ParentId",
    "Identity" = EXCLUDED."Identity",
    "SkinId" = EXCLUDED."SkinId";

INSERT INTO "WebSkin" ("Id", "Name", "Rank", "ObjectId", "RecordId")
VALUES (1, 'Default', 0, -1, -1)
ON CONFLICT ("Id") DO UPDATE SET
    "Name" = EXCLUDED."Name",
    "Rank" = EXCLUDED."Rank",
    "ObjectId" = EXCLUDED."ObjectId",
    "RecordId" = EXCLUDED."RecordId";

INSERT INTO "WebMasterPage" ("MasterPageId", "SiteId", "TemplateId", "Name", "OwnerPageId", "PublicAccess", "DateCreated", "DateModified", "ManagementAccess", "ThemeId", "SkinId")
VALUES (1, 1, 1, 'Default Master', -1, 1, NOW(), NOW(), 0, -1, -1)
ON CONFLICT ("MasterPageId") DO UPDATE SET
    "SiteId" = EXCLUDED."SiteId",
    "TemplateId" = EXCLUDED."TemplateId",
    "Name" = EXCLUDED."Name",
    "OwnerPageId" = EXCLUDED."OwnerPageId",
    "PublicAccess" = EXCLUDED."PublicAccess",
    "DateCreated" = EXCLUDED."DateCreated",
    "DateModified" = EXCLUDED."DateModified",
    "ManagementAccess" = EXCLUDED."ManagementAccess",
    "ThemeId" = EXCLUDED."ThemeId",
    "SkinId" = EXCLUDED."SkinId";

INSERT INTO "WebSite" ("SiteId", "Name", "Rank", "Active", "Identity", "Title", "ParentId", "HomePageId", "DefaultMasterPageId", "HostName", "Published", "VersionOf", "PublicAccess", "DateCreated", "DateModified", "LoginPage", "AccessDeniedPage", "PageTitleFormat", "ManagementAccess", "BaseAddress", "ThemeId", "LocaleId", "SkinId")
VALUES (1, 'Default', 1, 1, 'default', 'mPortal CMS', -1, 1, 1, 'localhost', -1, -1, 128, NOW(), NOW(), '', '', '', 0, '', -1, -1, -1)
ON CONFLICT ("SiteId") DO UPDATE SET
    "Name" = EXCLUDED."Name",
    "Rank" = EXCLUDED."Rank",
    "Active" = EXCLUDED."Active",
    "Identity" = EXCLUDED."Identity",
    "Title" = EXCLUDED."Title",
    "ParentId" = EXCLUDED."ParentId",
    "HomePageId" = EXCLUDED."HomePageId",
    "DefaultMasterPageId" = EXCLUDED."DefaultMasterPageId",
    "HostName" = EXCLUDED."HostName",
    "Published" = EXCLUDED."Published",
    "VersionOf" = EXCLUDED."VersionOf",
    "PublicAccess" = EXCLUDED."PublicAccess",
    "DateCreated" = EXCLUDED."DateCreated",
    "DateModified" = EXCLUDED."DateModified",
    "LoginPage" = EXCLUDED."LoginPage",
    "AccessDeniedPage" = EXCLUDED."AccessDeniedPage",
    "PageTitleFormat" = EXCLUDED."PageTitleFormat",
    "ManagementAccess" = EXCLUDED."ManagementAccess",
    "BaseAddress" = EXCLUDED."BaseAddress",
    "ThemeId" = EXCLUDED."ThemeId",
    "LocaleId" = EXCLUDED."LocaleId",
    "SkinId" = EXCLUDED."SkinId";

INSERT INTO "WebSiteIdentity" ("Id", "SiteId", "HostName", "UrlPath", "Port", "IPAddress", "RedirectUrl")
VALUES (1, 1, 'localhost', '', 5000, '', '')
ON CONFLICT ("Id") DO UPDATE SET
    "SiteId" = EXCLUDED."SiteId",
    "HostName" = EXCLUDED."HostName",
    "UrlPath" = EXCLUDED."UrlPath",
    "Port" = EXCLUDED."Port",
    "IPAddress" = EXCLUDED."IPAddress",
    "RedirectUrl" = EXCLUDED."RedirectUrl";

INSERT INTO "WebPage" ("PageId", "Name", "SiteId", "Rank", "Active", "Identity", "ParentId", "Title", "MasterPageId", "PartControlTemplateId", "Published", "VersionOfId", "PublicAccess", "DateCreated", "DateModified", "PageType", "UsePartTemplatePath", "ManagementAccess", "ThemeId", "LocaleId", "SkinId")
VALUES (1, 'Home', 1, 1, 1, 'home', -1, 'Welcome to mPortal', 1, -1, -1, -1, 128, NOW(), NOW(), 0, 1, 0, -1, -1, -1)
ON CONFLICT ("PageId") DO UPDATE SET
    "Name" = EXCLUDED."Name",
    "SiteId" = EXCLUDED."SiteId",
    "Rank" = EXCLUDED."Rank",
    "Active" = EXCLUDED."Active",
    "Identity" = EXCLUDED."Identity",
    "ParentId" = EXCLUDED."ParentId",
    "Title" = EXCLUDED."Title",
    "MasterPageId" = EXCLUDED."MasterPageId",
    "PartControlTemplateId" = EXCLUDED."PartControlTemplateId",
    "Published" = EXCLUDED."Published",
    "VersionOfId" = EXCLUDED."VersionOfId",
    "PublicAccess" = EXCLUDED."PublicAccess",
    "DateCreated" = EXCLUDED."DateCreated",
    "DateModified" = EXCLUDED."DateModified",
    "PageType" = EXCLUDED."PageType",
    "UsePartTemplatePath" = EXCLUDED."UsePartTemplatePath",
    "ManagementAccess" = EXCLUDED."ManagementAccess",
    "ThemeId" = EXCLUDED."ThemeId",
    "LocaleId" = EXCLUDED."LocaleId",
    "SkinId" = EXCLUDED."SkinId";

INSERT INTO "WebPagePanel" ("PagePanelId", "TemplatePanelId", "PageId", "UsageTypeId") VALUES
(1, 1, 1, -1),
(2, 2, 1, -1),
(3, 3, 1, -1)
ON CONFLICT ("PagePanelId") DO UPDATE SET
    "TemplatePanelId" = EXCLUDED."TemplatePanelId",
    "PageId" = EXCLUDED."PageId",
    "UsageTypeId" = EXCLUDED."UsageTypeId";

-- -----------------------------------------------------------------------------
-- 4) Baseline admin account (non-fixture; deterministic local bootstrap only)
-- -----------------------------------------------------------------------------
INSERT INTO "WebUser" (
    "UserId", "UserName", "Password", "FirstName", "MiddleName", "LastName", "Email",
    "LastUpdate", "Active", "ActivationKey", "DateCreated", "NewEmail", "Email2", "Gender",
    "NameSuffix", "MobileNumber", "TelephoneNumber", "LastLogin", "StatusText", "PasswordExpiryDate",
    "PhotoPath", "ProviderId", "Status"
)
VALUES (
    1, 'admin', 'admin', 'System', '', 'Administrator', 'admin@localhost',
    NOW(), 1, '', NOW(), '', '', 'U',
    '', '', '', NOW(), '', '2099-12-31 00:00:00',
    '', -1, 1
)
ON CONFLICT ("UserId") DO UPDATE SET
    "UserName" = EXCLUDED."UserName",
    "Password" = EXCLUDED."Password",
    "FirstName" = EXCLUDED."FirstName",
    "MiddleName" = EXCLUDED."MiddleName",
    "LastName" = EXCLUDED."LastName",
    "Email" = EXCLUDED."Email",
    "LastUpdate" = EXCLUDED."LastUpdate",
    "Active" = EXCLUDED."Active",
    "ActivationKey" = EXCLUDED."ActivationKey",
    "DateCreated" = EXCLUDED."DateCreated",
    "NewEmail" = EXCLUDED."NewEmail",
    "Email2" = EXCLUDED."Email2",
    "Gender" = EXCLUDED."Gender",
    "NameSuffix" = EXCLUDED."NameSuffix",
    "MobileNumber" = EXCLUDED."MobileNumber",
    "TelephoneNumber" = EXCLUDED."TelephoneNumber",
    "LastLogin" = EXCLUDED."LastLogin",
    "StatusText" = EXCLUDED."StatusText",
    "PasswordExpiryDate" = EXCLUDED."PasswordExpiryDate",
    "PhotoPath" = EXCLUDED."PhotoPath",
    "ProviderId" = EXCLUDED."ProviderId",
    "Status" = EXCLUDED."Status";

COMMIT;
