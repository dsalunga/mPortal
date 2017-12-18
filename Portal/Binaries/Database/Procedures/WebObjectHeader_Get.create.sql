
-- Procedure WebObjectHeader_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebObjectHeader_Get]
	(
		@ObjectHeaderId int = -1,
		@ObjectId int = -1,
		@RecordId int = -1,
		@TextResourceId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     ObjectHeaderId, ObjectId, RecordId, TextResourceId
	FROM         WebObjectHeader
	WHERE     
			(@ObjectHeaderId < 1 OR 
				ObjectHeaderId = @ObjectHeaderId)
		AND
			(@ObjectId < 1 OR 
				ObjectId = @ObjectId)
		AND
			(@RecordId < 1 OR 
				RecordId = @RecordId)
		AND
			(@TextResourceId < 1 OR 
				TextResourceId = @TextResourceId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

