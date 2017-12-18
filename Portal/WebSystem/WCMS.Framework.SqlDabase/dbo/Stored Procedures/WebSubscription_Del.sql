CREATE PROCEDURE dbo.WebSubscription_Del
	(
		@SubscriptionId int
	)
AS
	SET NOCOUNT ON
	
	IF(@SubscriptionId > 0)
		DELETE FROM WebSubscription
		WHERE SubscriptionId = @SubscriptionId
		
	RETURN