
if exists (select * from dbo.sysobjects where id = object_id(N'[MChapter_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MChapter_Set]
GO


