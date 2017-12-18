SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleLink]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ArticleLink](
	[Id] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[Style] [nvarchar](2500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[CommentOn] [int] NOT NULL,
 CONSTRAINT [PK_ArticleLink] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ArticleLi__Comment]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleLink] ADD  CONSTRAINT [DF__ArticleLi__Comment]  DEFAULT ((-1)) FOR [CommentOn]
END

GO
