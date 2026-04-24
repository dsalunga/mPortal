
if exists (select * from dbo.sysobjects where id = object_id(N'[WebUserGroup_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebUserGroup_Del]
GO


