
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPageElement_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPageElement_Set]
GO


