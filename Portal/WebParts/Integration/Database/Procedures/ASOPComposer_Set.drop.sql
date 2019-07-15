
if exists (select * from dbo.sysobjects where id = object_id(N'[MCComposer_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCComposer_Set]
GO


