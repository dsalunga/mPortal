
if exists (select * from dbo.sysobjects where id = object_id(N'[WebGlobalPolicy_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebGlobalPolicy_Del]
GO


