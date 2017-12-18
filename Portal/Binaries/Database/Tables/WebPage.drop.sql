IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_PartControlTemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_PartControlTemplateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_Published]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_Published]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_VersionOfId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_VersionOfId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_AuthMethodId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_AuthMethodId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_DateCreated]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_PageType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_PageType]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_UseTemplatePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPages_UseTemplatePath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPage_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPage_ManagementAccess]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPage_ThemeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF_WebPage_ThemeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPage__LocaleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF__WebPage__LocaleId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPage__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] DROP CONSTRAINT [DF__WebPage__SkinId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPage]') AND type in (N'U'))
DROP TABLE [dbo].[WebPage]
GO
