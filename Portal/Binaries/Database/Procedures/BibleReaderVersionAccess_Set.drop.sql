
if exists (select * from dbo.sysobjects where id = object_id(N'[BibleReaderVersionAccess_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [BibleReaderVersionAccess_Set]
GO


