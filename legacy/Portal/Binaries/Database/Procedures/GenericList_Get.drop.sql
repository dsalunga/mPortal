
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericList_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericList_Get]
GO


