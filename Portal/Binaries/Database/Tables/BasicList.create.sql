SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BasicList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BasicList](
	[BasicListID] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NULL,
	[PageType] [int] NULL,
	[SitePageItemID] [int] NULL,
	[RepeatColumns] [int] NULL,
	[ShowField2] [bit] NULL,
	[ShowField3] [bit] NULL,
	[CellPadding] [int] NULL,
	[ItemTemplate] [nvarchar](256) COLLATE Latin1_General_CI_AI NULL,
	[PageSize] [int] NULL,
	[GridLines] [int] NULL,
	[AlternatingColor] [nvarchar](64) COLLATE Latin1_General_CI_AI NULL,
	[TextColor] [nvarchar](64) COLLATE Latin1_General_CI_AI NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BasicLists] PRIMARY KEY CLUSTERED 
(
	[BasicListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
