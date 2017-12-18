
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleTemplate_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleTemplate_Del]
GO


