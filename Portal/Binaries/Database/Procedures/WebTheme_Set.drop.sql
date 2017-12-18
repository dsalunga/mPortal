
if exists (select * from dbo.sysobjects where id = object_id(N'[WebTheme_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebTheme_Set]
GO


