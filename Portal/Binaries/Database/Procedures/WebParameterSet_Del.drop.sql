
if exists (select * from dbo.sysobjects where id = object_id(N'[WebParameterSet_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebParameterSet_Del]
GO


