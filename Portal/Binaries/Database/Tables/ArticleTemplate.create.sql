SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ArticleTemplate](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Date] [datetime] NOT NULL,
	[File] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ImageUrl] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ListItemTemplate] [nvarchar](2500) COLLATE Latin1_General_CI_AI NOT NULL,
	[ListTemplate] [nvarchar](2500) COLLATE Latin1_General_CI_AI NOT NULL,
	[DetailsTemplate] [nvarchar](2500) COLLATE Latin1_General_CI_AI NOT NULL,
	[DateFormat] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleTemplate_ItemTemplate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] ADD  CONSTRAINT [DF_ArticleTemplate_ItemTemplate]  DEFAULT ('') FOR [ListItemTemplate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleTemplate_ListTemplate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] ADD  CONSTRAINT [DF_ArticleTemplate_ListTemplate]  DEFAULT ('') FOR [ListTemplate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleTemplate_DetailsTemplate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] ADD  CONSTRAINT [DF_ArticleTemplate_DetailsTemplate]  DEFAULT ('') FOR [DetailsTemplate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ArticleTe__DateFormat]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleTemplate] ADD  CONSTRAINT [DF__ArticleTe__DateFormat]  DEFAULT ('') FOR [DateFormat]
END

GO
