
if exists (select * from dbo.sysobjects where id = object_id(N'[WebTemplate_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebTemplate_Set]
GO


