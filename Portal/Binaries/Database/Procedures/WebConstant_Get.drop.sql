
if exists (select * from dbo.sysobjects where id = object_id(N'[WebConstant_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebConstant_Get]
GO


