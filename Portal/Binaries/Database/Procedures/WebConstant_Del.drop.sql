
if exists (select * from dbo.sysobjects where id = object_id(N'[WebConstant_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebConstant_Del]
GO


