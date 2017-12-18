IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplatePanels_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] DROP CONSTRAINT [DF_WebTemplatePanels_TemplateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplatePanel_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] DROP CONSTRAINT [DF_WebTemplatePanel_Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] DROP CONSTRAINT [DF__WebTempla__ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] DROP CONSTRAINT [DF__WebTempla__RecordId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTemplatePanel]') AND type in (N'U'))
DROP TABLE [dbo].[WebTemplatePanel]
GO
