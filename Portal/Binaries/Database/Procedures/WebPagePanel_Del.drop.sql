
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPagePanel_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPagePanel_Del]
GO


