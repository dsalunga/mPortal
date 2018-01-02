
if exists (select * from dbo.sysobjects where id = object_id(N'[BibleReaderVersionAccess_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [BibleReaderVersionAccess_Del]
GO


