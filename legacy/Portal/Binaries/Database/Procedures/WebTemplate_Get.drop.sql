
if exists (select * from dbo.sysobjects where id = object_id(N'[WebTemplate_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebTemplate_Get]
GO


