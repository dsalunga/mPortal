
if exists (select * from dbo.sysobjects where id = object_id(N'[WebRole_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebRole_Set]
GO


