
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryCategoryLink_GetTypeIdOut]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryCategoryLink_GetTypeIdOut]
GO


