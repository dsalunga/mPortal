IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_Width]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] DROP CONSTRAINT [DF_GalleryCategory_Width]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_PhotoHeight]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] DROP CONSTRAINT [DF_GalleryCategory_PhotoHeight]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_FolderName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] DROP CONSTRAINT [DF_GalleryCategory_FolderName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_PhotoWidth]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] DROP CONSTRAINT [DF_GalleryCategory_PhotoWidth]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GalleryCategory_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GalleryCategory] DROP CONSTRAINT [DF_GalleryCategory_DateModified]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GalleryCategory]') AND type in (N'U'))
DROP TABLE [dbo].[GalleryCategory]
GO
