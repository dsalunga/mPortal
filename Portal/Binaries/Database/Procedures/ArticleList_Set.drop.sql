
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleList_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleList_Set]
GO


