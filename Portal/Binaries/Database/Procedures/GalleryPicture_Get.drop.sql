
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryPicture_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryPicture_Get]
GO


