SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StdMenuProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StdMenuProperties](
	[ListingPagePropertyID] [int] IDENTITY(1,1) NOT NULL,
	[PageType] [int] NULL,
	[SitePageItemID] [int] NULL,
	[RepeatColumns] [int] NULL,
	[HeaderText] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParentID] [int] NULL,
	[ListingType] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CellPadding] [int] NULL,
 CONSTRAINT [PK_ListingPageProperties] PRIMARY KEY CLUSTERED 
(
	[ListingPagePropertyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
