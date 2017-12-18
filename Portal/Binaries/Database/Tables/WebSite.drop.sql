IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_Published]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_Published]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_VersionOfId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_VersionOfId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_AuthenticationTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_AuthenticationTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_DateCreated]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_LoginPage]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_LoginPage]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_AccessDeniedPage]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSites_AccessDeniedPage]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_PageTitleFormat]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSite_PageTitleFormat]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSite_ManagementAccess]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_BaseAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSite_BaseAddress]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSite__ThemeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF__WebSite__ThemeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSite__LocaleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF__WebSite__LocaleId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSite__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF__WebSite__SkinId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_PrimaryIdentityId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] DROP CONSTRAINT [DF_WebSite_PrimaryIdentityId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSite]') AND type in (N'U'))
DROP TABLE [dbo].[WebSite]
GO
