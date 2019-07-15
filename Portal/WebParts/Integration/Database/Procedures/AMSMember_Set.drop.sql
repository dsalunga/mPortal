
if exists (select * from dbo.sysobjects where id = object_id(N'[ExternalMember_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ExternalMember_Set]
GO


