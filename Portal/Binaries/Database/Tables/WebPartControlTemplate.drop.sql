IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControlTemplates_CompletePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControlTemplate] DROP CONSTRAINT [DF_WebPartControlTemplates_CompletePath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPartControlTemplate__Standalone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControlTemplate] DROP CONSTRAINT [DF__WebPartControlTemplate__Standalone]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControlTemplate_EngineId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControlTemplate] DROP CONSTRAINT [DF_WebPartControlTemplate_EngineId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPartControlTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[WebPartControlTemplate]
GO
