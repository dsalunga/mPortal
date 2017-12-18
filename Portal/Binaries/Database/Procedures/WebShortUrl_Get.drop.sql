
if exists (select * from dbo.sysobjects where id = object_id(N'[WebShortUrl_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebShortUrl_Get]
GO


