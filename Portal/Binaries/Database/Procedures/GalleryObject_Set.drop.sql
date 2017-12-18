
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryObject_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryObject_Set]
GO


