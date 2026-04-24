
if exists (select * from dbo.sysobjects where id = object_id(N'[WebOffice_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebOffice_Del]
GO


