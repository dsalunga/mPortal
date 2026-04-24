
if exists (select * from dbo.sysobjects where id = object_id(N'[WebShortUrl_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebShortUrl_Del]
GO


