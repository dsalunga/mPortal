IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryProperty_MaxPhotoWidth]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryObject] DROP CONSTRAINT [DF_GalleryProperty_MaxPhotoWidth]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GalleryObject]') AND type in (N'U'))
DROP TABLE [dbo].[GalleryObject]
GO
