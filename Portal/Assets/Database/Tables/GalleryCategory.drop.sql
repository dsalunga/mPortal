IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GalleryCategory]') AND type in (N'U'))
DROP TABLE [dbo].[GalleryCategory]
GO
