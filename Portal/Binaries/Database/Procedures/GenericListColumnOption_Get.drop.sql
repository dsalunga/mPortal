
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListColumnOption_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListColumnOption_Get]
GO


