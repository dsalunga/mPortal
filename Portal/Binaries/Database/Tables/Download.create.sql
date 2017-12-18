SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Download]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Download](
	[DownloadID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileDate] [datetime] NULL,
	[Filename] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateModified] [datetime] NULL,
	[Rank] [int] NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Download] PRIMARY KEY CLUSTERED 
(
	[DownloadID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
