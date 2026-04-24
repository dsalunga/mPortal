
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPagePanel_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPagePanel_Get]
GO


