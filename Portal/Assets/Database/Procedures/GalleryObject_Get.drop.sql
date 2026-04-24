
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryObject_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryObject_Get]
GO


