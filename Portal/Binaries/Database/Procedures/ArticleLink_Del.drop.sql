
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleLink_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleLink_Del]
GO


