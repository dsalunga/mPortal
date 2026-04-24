
if exists (select * from dbo.sysobjects where id = object_id(N'[FileVersion_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [FileVersion_Get]
GO


