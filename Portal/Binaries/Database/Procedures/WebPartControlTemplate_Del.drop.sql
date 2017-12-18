
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartControlTemplate_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartControlTemplate_Del]
GO


