SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GalleryObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GalleryObject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[InitialControl] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ThumbColumns] [int] NOT NULL,
	[ThumbRows] [int] NOT NULL,
	[AlbumColumns] [int] NOT NULL,
	[AlbumCellPadding] [int] NOT NULL,
	[MaxPhotoWidth] [int] NOT NULL,
 CONSTRAINT [PK_SiteProperty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryProperty_MaxPhotoWidth]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryObject] ADD  CONSTRAINT [DF_GalleryProperty_MaxPhotoWidth]  DEFAULT ((700)) FOR [MaxPhotoWidth]
END

GO
