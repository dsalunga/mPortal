
if exists (select * from dbo.sysobjects where id = object_id(N'[MCInterpreterScore_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCInterpreterScore_Del]
GO


