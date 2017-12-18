
-- Procedure WebSubscription_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebSubscription_Set
	(
		@SubscriptionId int = -1,
		@ObjectId int,
		@RecordId int,
		@PartId int,
		@PageId int,
		@Allow int
	)
AS
	SET NOCOUNT ON
	
	IF(@SubscriptionId > 0)
		BEGIN
			-- Update
			UPDATE       WebSubscription
			SET                SubscriptionId = @SubscriptionId, ObjectId = @ObjectId, RecordId = @RecordId, PartId = @PartId, PageId = @PageId, Allow = @Allow
			WHERE SubscriptionId = @SubscriptionId
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @SubscriptionId = WebObject_NextId 'WebSubscription'
			
			INSERT INTO WebSubscription
			                         (SubscriptionId, ObjectId, RecordId, PartId, PageId, Allow)
			VALUES        (@SubscriptionId,@ObjectId,@RecordId,@PartId,@PageId,@Allow)
		END
		
	SELECT @SubscriptionId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

