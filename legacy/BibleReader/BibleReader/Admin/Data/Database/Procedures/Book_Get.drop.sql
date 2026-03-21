
if exists (select * from dbo.sysobjects where id = object_id(N'[Book_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Book_Get]
GO


