
if exists (select * from dbo.sysobjects where id = object_id(N'[WebUserRole_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebUserRole_Get]
GO


