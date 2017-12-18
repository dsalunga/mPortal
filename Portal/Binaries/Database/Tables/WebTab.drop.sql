IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Text]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] DROP CONSTRAINT [DF_WebTab_Text]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Target]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] DROP CONSTRAINT [DF_WebTab_Target]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] DROP CONSTRAINT [DF_WebTab_Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_TabSetId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] DROP CONSTRAINT [DF_WebTab_TabSetId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] DROP CONSTRAINT [DF_WebTab_Name]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTab]') AND type in (N'U'))
DROP TABLE [dbo].[WebTab]
GO
