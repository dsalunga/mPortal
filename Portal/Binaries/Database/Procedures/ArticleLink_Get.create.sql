
-- Procedure ArticleLink_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ArticleLink_Get]
	(
		@ObjectId int = -2,
		@RecordId int = -2,
		@ArticleId int = -2,
		@SiteId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Rank, Style, ObjectId, RecordId, ArticleId, SiteId, CommentOn
	FROM         ArticleLink
	WHERE
		(@ObjectId = -2 OR ObjectId=@ObjectId) AND
		(@RecordId=-2 OR RecordId=@RecordId) AND
		(@SiteId=-2 OR SiteId=@SiteId) AND
		(@ArticleId=-2 OR ArticleId=@ArticleId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

