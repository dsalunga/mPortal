
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericListLink_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericListLink_Get]
GO


