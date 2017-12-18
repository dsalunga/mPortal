
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListRow_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListRow_Set]
GO


