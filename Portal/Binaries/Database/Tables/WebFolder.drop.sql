IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebFolder_ShareName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebFolder] DROP CONSTRAINT [DF_WebFolder_ShareName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebFolder_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebFolder] DROP CONSTRAINT [DF_WebFolder_ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebFolder_SiteId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebFolder] DROP CONSTRAINT [DF_WebFolder_SiteId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebFolder]') AND type in (N'U'))
DROP TABLE [dbo].[WebFolder]
GO
