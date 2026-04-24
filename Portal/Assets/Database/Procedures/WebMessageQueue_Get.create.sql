
-- Procedure WebMessageQueue_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebMessageQueue_Get
	(
		@Id int = -1,
		@Status int = -2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, FromObjectId, FromRecordId, EmailMessage, SmsMessage, [To], ToFailed, ToExcluded, ToOrBcc, DateCreated, 
				DateSent, Status, SendVia, EmailSubject, EnableMonitor
	FROM         WebMessageQueue
	WHERE     (@Id=-1 OR Id = @Id)
		AND (@Status = -2 OR Status=@Status)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

