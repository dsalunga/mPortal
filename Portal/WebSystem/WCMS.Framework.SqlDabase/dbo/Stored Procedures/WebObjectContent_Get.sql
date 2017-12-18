﻿CREATE PROCEDURE [dbo].[WebObjectContent_Get]
	(
		@ObjectContentId int = -1,
		@ObjectId int = -1,
		@RecordId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     ObjectContentId, ObjectId, ContentId, RecordId
	FROM         WebObjectContent
	WHERE     
			(@ObjectContentId = - 1 OR ObjectContentId = @ObjectContentId) 
		AND (@ObjectId = - 1 OR ObjectId = @ObjectId) 
		AND (@RecordId = - 1 OR RecordId = @RecordId)
	
	RETURN