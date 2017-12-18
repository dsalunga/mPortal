IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplates_PrimaryPanelId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF_WebTemplates_PrimaryPanelId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_Version]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF_WebTemplate_Version]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_LatestVersion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF_WebTemplate_LatestVersion]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF_WebTemplate_Content]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF_WebTemplate_SkinId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__Standalone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF__WebTempla__Standalone]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF__WebTempla__ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF__WebTempla__SkinId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__TemplateEngineId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] DROP CONSTRAINT [DF__WebTempla__TemplateEngineId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[WebTemplate]
GO
