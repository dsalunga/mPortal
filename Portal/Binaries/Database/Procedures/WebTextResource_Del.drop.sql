
if exists (select * from dbo.sysobjects where id = object_id(N'[WebTextResource_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebTextResource_Del]
GO


