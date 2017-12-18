
-- Procedure WebSubscription_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebSubscription_Get
	(
		@SubscriptionId int = -1,
		@ObjectId int = -1,
		@RecordId int = -1,
		@PartId int = -1,
		@PageId int = -1,
		@Allow int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        SubscriptionId, ObjectId, RecordId, PartId, PageId, Allow
	FROM            WebSubscription
	WHERE
		(@SubscriptionId =-1 OR SubscriptionId=@SubscriptionId) AND
		(@ObjectId = -1 OR ObjectId = @ObjectId) AND
		(@RecordId = -1 OR RecordId = @RecordId) AND
		(@PartId = -1 OR PartId = @PartId) AND
		(@PageId = -1 OR PageId=@PageId) AND
		(@Allow = -1 OR Allow=@Allow)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

