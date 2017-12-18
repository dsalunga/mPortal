
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartConfig_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartConfig_Del]
GO


