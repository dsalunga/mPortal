
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSite_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSite_Get]
GO


