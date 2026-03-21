
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListColumn_GetPartition]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListColumn_GetPartition]
GO


