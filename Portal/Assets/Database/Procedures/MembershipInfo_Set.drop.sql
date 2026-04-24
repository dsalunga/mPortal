
if exists (select * from dbo.sysobjects where id = object_id(N'[MembershipInfo_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MembershipInfo_Set]
GO


