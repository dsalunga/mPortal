
if exists (select * from dbo.sysobjects where id = object_id(N'[GalleryCategoryLink_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GalleryCategoryLink_Get]
GO


