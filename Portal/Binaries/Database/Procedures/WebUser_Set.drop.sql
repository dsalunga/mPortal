
if exists (select * from dbo.sysobjects where id = object_id(N'[WebUser_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebUser_Set]
GO


