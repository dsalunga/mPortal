CREATE PROCEDURE dbo.WebMessageQueue_Set
	(
		@Id int = -1,
		@FromObjectId int,
		@FromRecordId int,
		@EmailMessage ntext,
		@EmailSubject nvarchar(4000),
		@SmsMessage nvarchar(4000),
		@To nvarchar(4000),
		@ToExcluded nvarchar(4000),
		@ToFailed nvarchar(4000),
		@ToOrBcc int,
		@DateCreated datetime,
		@DateSent datetime,
		@Status int,
		@SendVia int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebMessageQueue
			SET              FromObjectId = @FromObjectId, FromRecordId = @FromRecordId, EmailMessage = @EmailMessage, SmsMessage = @SmsMessage, 
								  [To] = @To, ToExcluded=@ToExcluded, ToFailed = @ToFailed, ToOrBcc = @ToOrBcc, DateCreated = @DateCreated, 
								  DateSent = @DateSent, Status = @Status, SendVia=@SendVia, EmailSubject=@EmailSubject
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebMessageQueue'

			INSERT INTO WebMessageQueue
			                         (Id, FromObjectId, FromRecordId, EmailMessage, SmsMessage, [To], ToExcluded, ToFailed, ToOrBcc, DateCreated, 
									 DateSent, Status, SendVia, EmailSubject)
			VALUES        (@Id,@FromObjectId,@FromRecordId,@EmailMessage,@SmsMessage,@To,@ToExcluded,@ToFailed,@ToOrBcc,@DateCreated,@DateSent,
								@Status, @SendVia, @EmailSubject)
		END

	SELECT @Id

	RETURN