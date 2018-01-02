
if exists (select * from dbo.sysobjects where id = object_id(N'[MemberLink_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MemberLink_Get]
GO


