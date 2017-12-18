SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gallery]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gallery](
	[GalleryID] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Thumbnail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ImageURL] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NULL,
	[SiteID] [int] NULL,
	[CategoryID] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[GalleryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
