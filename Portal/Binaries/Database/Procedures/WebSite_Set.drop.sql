
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSite_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSite_Set]
GO


