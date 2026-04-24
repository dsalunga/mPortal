
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryLink_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryLink_Get]
GO


