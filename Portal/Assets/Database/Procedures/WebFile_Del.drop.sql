
if exists (select * from dbo.sysobjects where id = object_id(N'[WebFile_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebFile_Del]
GO


