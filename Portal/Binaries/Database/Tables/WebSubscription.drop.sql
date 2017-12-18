IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSubscription_PartId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSubscription] DROP CONSTRAINT [DF_WebSubscription_PartId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleSubscription_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSubscription] DROP CONSTRAINT [DF_ArticleSubscription_PageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleSubscription_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSubscription] DROP CONSTRAINT [DF_ArticleSubscription_Allow]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSubscription]') AND type in (N'U'))
DROP TABLE [dbo].[WebSubscription]
GO
