
if exists (select * from dbo.sysobjects where id = object_id(N'[MCComposer_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCComposer_Get]
GO


