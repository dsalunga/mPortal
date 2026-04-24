
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleTemplate_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleTemplate_Set]
GO


