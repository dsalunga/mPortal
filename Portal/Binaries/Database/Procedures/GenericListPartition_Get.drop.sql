
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListPartition_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListPartition_Get]
GO


