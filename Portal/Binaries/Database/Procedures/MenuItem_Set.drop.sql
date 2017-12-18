
if exists (select * from dbo.sysobjects where id = object_id(N'[MenuItem_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MenuItem_Set]
GO


