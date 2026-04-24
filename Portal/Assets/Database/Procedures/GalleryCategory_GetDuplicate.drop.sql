
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryCategory_GetDuplicate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryCategory_GetDuplicate]
GO


