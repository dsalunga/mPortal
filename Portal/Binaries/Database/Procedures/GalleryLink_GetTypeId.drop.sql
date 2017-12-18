
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryLink_GetTypeId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryLink_GetTypeId]
GO


