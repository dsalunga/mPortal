
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSkin_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSkin_Get]
GO


