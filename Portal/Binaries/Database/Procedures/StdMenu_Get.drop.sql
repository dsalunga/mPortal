
if exists (select * from dbo.sysobjects where id = object_id(N'[StdMenu_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [StdMenu_Get]
GO


