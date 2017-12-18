
if exists (select * from dbo.sysobjects where id = object_id(N'[WebGlobalPolicy_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebGlobalPolicy_Get]
GO


