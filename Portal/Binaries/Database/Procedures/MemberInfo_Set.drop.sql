
if exists (select * from dbo.sysobjects where id = object_id(N'[MemberInfo_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MemberInfo_Set]
GO


