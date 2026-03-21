CREATE PROCEDURE dbo.LessonReviewerSession_Get
	(
		@Id int = -1,
		@MemberId int = -1,
		@Status int = -1,
		@DateStart datetime = NULL,
		@DateEnd datetime = NULL
	)
AS
	SET NOCOUNT ON

	SELECT     Id, ServiceScheduleID, ServiceStartDate, ServiceName, DateStarted, DateCompleted, MemberId, AbsentReason, CouncillorNotes, CouncillorUserId, Status, 
						  DateApproved, AdditionalNotes
	FROM         LessonReviewerSession
	WHERE     (@Id=-1 OR Id = @Id)
		AND	(@Status=-1 OR Status=@Status)
		AND	(@MemberId=-1 OR MemberId=@MemberId)
		AND	(@DateStart IS NULL OR @DateEnd IS NULL OR
				(ServiceStartDate >= @DateStart AND ServiceStartDate <= @DateEnd)
			)
	ORDER BY DateStarted DESC

	RETURN