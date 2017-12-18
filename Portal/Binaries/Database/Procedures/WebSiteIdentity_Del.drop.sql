
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSiteIdentity_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSiteIdentity_Del]
GO


