
-- Procedure WebSubscription_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

