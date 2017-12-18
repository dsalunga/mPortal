
if exists (select * from dbo.sysobjects where id = object_id(N'[ArticleLink_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ArticleLink_Get]
GO


