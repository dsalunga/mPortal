
if exists (select * from dbo.sysobjects where id = object_id(N'[WebFile_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebFile_Set]
GO


