SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DownloadProperty]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DownloadProperty](
	[DownloadPropertyID] [int] IDENTITY(1,1) NOT NULL,
	[PageType] [int] NULL,
	[SitePageItemID] [int] NULL,
	[InitialControl] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Columns] [int] NULL,
	[Rows] [int] NULL,
	[MaxRecords] [int] NULL,
	[ForceDownload] [bit] NULL,
 CONSTRAINT [PK_DownloadProperty] PRIMARY KEY CLUSTERED 
(
	[DownloadPropertyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
