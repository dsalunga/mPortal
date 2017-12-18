IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_OwnerPageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPages_OwnerPageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_AuthenticationTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPages_AuthenticationTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPages_DateCreated]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPages_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPage_ManagementAccess]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage_ThemeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPage_ThemeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPage__SkinId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] DROP CONSTRAINT [DF_WebMasterPage_ParentId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebMasterPage]') AND type in (N'U'))
DROP TABLE [dbo].[WebMasterPage]
GO
