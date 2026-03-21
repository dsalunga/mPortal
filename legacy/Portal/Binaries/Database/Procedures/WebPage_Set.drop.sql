
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPage_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPage_Set]
GO


