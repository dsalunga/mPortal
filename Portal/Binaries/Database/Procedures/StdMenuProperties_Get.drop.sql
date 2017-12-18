
if exists (select * from dbo.sysobjects where id = object_id(N'[StdMenuProperties_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [StdMenuProperties_Get]
GO


