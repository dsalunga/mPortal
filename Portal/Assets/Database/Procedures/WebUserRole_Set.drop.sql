
if exists (select * from dbo.sysobjects where id = object_id(N'[WebUserRole_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebUserRole_Set]
GO


