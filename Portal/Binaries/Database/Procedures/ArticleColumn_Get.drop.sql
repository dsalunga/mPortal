
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleColumn_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleColumn_Get]
GO


