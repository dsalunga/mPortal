
if exists (select * from dbo.sysobjects where id = object_id(N'[WebUserRole_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebUserRole_Del]
GO


