
if exists (select * from dbo.sysobjects where id = object_id(N'[WebTextResource_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebTextResource_Set]
GO


