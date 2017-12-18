IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTheme_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] DROP CONSTRAINT [DF_WebTheme_TemplateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTheme_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] DROP CONSTRAINT [DF_WebTheme_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTheme__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] DROP CONSTRAINT [DF__WebTheme__ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTheme__Identiy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] DROP CONSTRAINT [DF__WebTheme__Identiy]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTheme__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] DROP CONSTRAINT [DF__WebTheme__SkinId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTheme]') AND type in (N'U'))
DROP TABLE [dbo].[WebTheme]
GO
