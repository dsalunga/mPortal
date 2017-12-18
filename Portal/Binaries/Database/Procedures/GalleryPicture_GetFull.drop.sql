
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryPicture_GetFull]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryPicture_GetFull]
GO


