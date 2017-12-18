
if exists (select * from dbo.sysobjects where id = object_id(N'[BasicListItem_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [BasicListItem_Set]
GO


