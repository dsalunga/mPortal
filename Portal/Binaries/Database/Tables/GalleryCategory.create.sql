SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GalleryCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GalleryCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ImageURL] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Width] [int] NOT NULL,
	[PhotoHeight] [int] NOT NULL,
	[FolderName] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[PhotoWidth] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_GalleryCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_Width]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] ADD  CONSTRAINT [DF_GalleryCategory_Width]  DEFAULT ((-1)) FOR [Width]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_PhotoHeight]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] ADD  CONSTRAINT [DF_GalleryCategory_PhotoHeight]  DEFAULT ((75)) FOR [PhotoHeight]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_FolderName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] ADD  CONSTRAINT [DF_GalleryCategory_FolderName]  DEFAULT ('') FOR [FolderName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_PhotoWidth]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] ADD  CONSTRAINT [DF_GalleryCategory_PhotoWidth]  DEFAULT ((112)) FOR [PhotoWidth]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] ADD  CONSTRAINT [DF_GalleryCategory_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
