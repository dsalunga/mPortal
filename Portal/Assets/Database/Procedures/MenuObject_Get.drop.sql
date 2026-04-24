
if exists (select * from dbo.sysobjects where id = object_id(N'[MenuObject_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MenuObject_Get]
GO


