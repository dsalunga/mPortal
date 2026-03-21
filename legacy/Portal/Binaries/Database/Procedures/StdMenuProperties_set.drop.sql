
if exists (select * from dbo.sysobjects where id = object_id(N'[StdMenuProperties_set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [StdMenuProperties_set]
GO


