
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleList_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleList_Get]
GO


