CREATE PROCEDURE dbo.WebMessageQueue_Get
	(
		@Id int = -1,
		@Status int = -2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, FromObjectId, FromRecordId, EmailMessage, SmsMessage, [To], ToFailed, ToExcluded, ToOrBcc, DateCreated, 
				DateSent, Status, SendVia, EmailSubject
	FROM         WebMessageQueue
	WHERE     (@Id=-1 OR Id = @Id)
		AND (@Status = -2 OR Status=@Status)

	RETURN