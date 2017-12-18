SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BasicListItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BasicListItem](
	[ListItemID] [int] IDENTITY(1,1) NOT NULL,
	[PageType] [int] NULL,
	[SitePageItemID] [int] NULL,
	[Field1] [nvarchar](255) COLLATE Latin1_General_CI_AI NULL,
	[Field2] [nvarchar](255) COLLATE Latin1_General_CI_AI NULL,
	[Field3] [nvarchar](255) COLLATE Latin1_General_CI_AI NULL,
	[Rank] [int] NULL,
 CONSTRAINT [PK_ListItems] PRIMARY KEY CLUSTERED 
(
	[ListItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
