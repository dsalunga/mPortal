
if exists (select * from dbo.sysobjects where id = object_id(N'[UserProvider_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [UserProvider_Get]
GO


