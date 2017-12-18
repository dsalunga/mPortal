SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ArticleList](
	[ListId] [int] NOT NULL,
	[PageSize] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[FolderId] [int] NOT NULL,
	[CommentOn] [int] NOT NULL,
 CONSTRAINT [PK_ArticleList] PRIMARY KEY CLUSTERED 
(
	[ListId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleList_FolderId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleList] ADD  CONSTRAINT [DF_ArticleList_FolderId]  DEFAULT ((-1)) FOR [FolderId]
END

GO
