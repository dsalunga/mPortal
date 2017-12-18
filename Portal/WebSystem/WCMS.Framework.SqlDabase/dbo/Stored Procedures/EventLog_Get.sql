CREATE PROCEDURE dbo.EventLog_Get
	(
		@Id int = -1,
		@UserId int = -1,
		@EventName nvarchar(250) = NULL,
		@EventDate datetime = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, EventDate, [Content], UserId, EventName, IPAddress
	FROM            EventLog
	WHERE
			(@Id = -1 OR Id=@Id)
		AND	(@UserId = -1 OR UserId=@UserId)
		AND (@EventName IS NULL OR EventName=@EventName)
		AND (@EventDate IS NULL OR EventDate>=@EventDate)
	ORDER BY Id DESC

	RETURN