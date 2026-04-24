
if exists (select * from dbo.sysobjects where id = object_id(N'[MCVote_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCVote_Set]
GO


