
if exists (select * from dbo.sysobjects where id = object_id(N'[MCCandidate_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCCandidate_Get]
GO


