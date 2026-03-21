
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListRow_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListRow_Get]
GO


