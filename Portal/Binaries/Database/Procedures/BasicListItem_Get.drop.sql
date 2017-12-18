
if exists (select * from dbo.sysobjects where id = object_id(N'[BasicListItem_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [BasicListItem_Get]
GO


