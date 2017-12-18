IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebShortUrl_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShortUrl] DROP CONSTRAINT [DF_WebShortUrl_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebShortUrl_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShortUrl] DROP CONSTRAINT [DF_WebShortUrl_PageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebShortU_PageUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShortUrl] DROP CONSTRAINT [DF_WebShortU_PageUrl]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebShortUrl]') AND type in (N'U'))
DROP TABLE [dbo].[WebShortUrl]
GO
