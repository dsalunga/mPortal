IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleList_FolderId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleList] DROP CONSTRAINT [DF_ArticleList_FolderId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleList]') AND type in (N'U'))
DROP TABLE [dbo].[ArticleList]
GO
