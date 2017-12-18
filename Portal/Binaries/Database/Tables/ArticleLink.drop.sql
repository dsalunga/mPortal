IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ArticleLi__Comment]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleLink] DROP CONSTRAINT [DF__ArticleLi__Comment]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleLink]') AND type in (N'U'))
DROP TABLE [dbo].[ArticleLink]
GO
