
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSkin_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSkin_Set]
GO


