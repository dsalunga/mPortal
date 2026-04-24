
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSite_GetCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSite_GetCount]
GO


