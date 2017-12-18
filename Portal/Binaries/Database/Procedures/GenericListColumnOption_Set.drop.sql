
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListColumnOption_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListColumnOption_Set]
GO


