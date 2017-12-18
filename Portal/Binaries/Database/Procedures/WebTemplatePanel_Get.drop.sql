
if exists (select * from dbo.sysobjects where id = object_id(N'[WebTemplatePanel_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebTemplatePanel_Get]
GO


