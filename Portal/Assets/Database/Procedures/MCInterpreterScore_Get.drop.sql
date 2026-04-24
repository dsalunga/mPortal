
if exists (select * from dbo.sysobjects where id = object_id(N'[MCInterpreterScore_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCInterpreterScore_Get]
GO


