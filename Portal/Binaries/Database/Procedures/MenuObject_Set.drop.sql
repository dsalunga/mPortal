
if exists (select * from dbo.sysobjects where id = object_id(N'[MenuObject_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MenuObject_Set]
GO


