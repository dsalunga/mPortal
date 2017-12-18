
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericList_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericList_Set]
GO


