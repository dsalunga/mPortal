
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPermissionSet_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPermissionSet_Get]
GO


