
if exists (select * from dbo.sysobjects where id = object_id(N'[WebRole_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebRole_Get]
GO


