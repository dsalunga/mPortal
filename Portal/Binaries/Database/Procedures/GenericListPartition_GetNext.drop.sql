
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListPartition_GetNext]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListPartition_GetNext]
GO


