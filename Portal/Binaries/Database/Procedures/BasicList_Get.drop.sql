
if exists (select * from dbo.sysobjects where id = object_id(N'[BasicList_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [BasicList_Get]
GO


