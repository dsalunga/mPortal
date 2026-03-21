
if exists (select * from dbo.sysobjects where id = object_id(N'[Articles_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Articles_Get]
GO


