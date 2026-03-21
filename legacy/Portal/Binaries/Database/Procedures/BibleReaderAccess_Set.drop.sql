
if exists (select * from dbo.sysobjects where id = object_id(N'[BibleReaderAccess_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [BibleReaderAccess_Set]
GO


