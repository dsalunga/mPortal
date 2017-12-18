IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleTemplate_ItemTemplate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] DROP CONSTRAINT [DF_ArticleTemplate_ItemTemplate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleTemplate_ListTemplate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] DROP CONSTRAINT [DF_ArticleTemplate_ListTemplate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleTemplate_DetailsTemplate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] DROP CONSTRAINT [DF_ArticleTemplate_DetailsTemplate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ArticleTe__DateFormat]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] DROP CONSTRAINT [DF__ArticleTe__DateFormat]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[ArticleTemplate]
GO
