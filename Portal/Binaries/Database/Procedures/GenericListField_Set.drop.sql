
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListField_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListField_Set]
GO


