
if exists (select * from dbo.sysobjects where id = object_id(N'[WebOffice_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebOffice_Get]
GO


