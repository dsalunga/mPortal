
if exists (select * from dbo.sysobjects where id = object_id(N'[WallUpdate_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WallUpdate_Del]
GO


