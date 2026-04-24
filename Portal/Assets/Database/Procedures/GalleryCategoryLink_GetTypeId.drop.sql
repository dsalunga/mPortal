
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryCategoryLink_GetTypeId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryCategoryLink_GetTypeId]
GO


