IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContents_VersionOfId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContents_VersionOfId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContents_VersionNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContents_VersionNo]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContents_DirectoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContents_DirectoryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContent_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_ContentTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContent_ContentTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContent_Active]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_SiteId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebContent_SiteId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebConten__EditorSensitive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF__WebConten__EditorSensitive]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebConten_ActiveContent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] DROP CONSTRAINT [DF_WebConten_ActiveContent]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebContent]') AND type in (N'U'))
DROP TABLE [dbo].[WebContent]
GO
