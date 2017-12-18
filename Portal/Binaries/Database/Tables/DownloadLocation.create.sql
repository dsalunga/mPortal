SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DownloadLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DownloadLocation](
	[DownloadLocationID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NULL,
	[PageType] [int] NULL,
	[SitePageItemID] [int] NULL,
	[DownloadID] [int] NULL,
 CONSTRAINT [PK_DownloadLocation_1] PRIMARY KEY CLUSTERED 
(
	[DownloadLocationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
