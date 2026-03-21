
if exists (select * from dbo.sysobjects where id = object_id(N'[WallPlugin_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WallPlugin_Get]
GO


