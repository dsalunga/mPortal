
if exists (select * from dbo.sysobjects where id = object_id(N'[MenuItem_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MenuItem_Del]
GO


