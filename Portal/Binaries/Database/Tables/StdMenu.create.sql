SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StdMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StdMenu](
	[StdMenuID] [int] IDENTITY(1,1) NOT NULL,
	[SitePageItemID] [int] NULL,
	[PageType] [int] NULL,
	[Width] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Height] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Horizontal] [bit] NULL,
	[SiteID] [int] NULL,
	[ShowHome] [bit] NULL,
	[SiteSectionID] [int] NULL,
	[HomeText] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_StdMenu] PRIMARY KEY CLUSTERED 
(
	[StdMenuID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
