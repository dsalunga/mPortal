
if exists (select * from dbo.sysobjects where id = object_id(N'[FileIdentity_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [FileIdentity_Get]
GO


