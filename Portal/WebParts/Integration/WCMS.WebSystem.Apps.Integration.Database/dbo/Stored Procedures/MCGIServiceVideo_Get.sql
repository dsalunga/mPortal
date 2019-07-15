CREATE PROCEDURE dbo.LessonReviewerVideo_Get
	(
		@ServiceScheduleId int = -1,
		@Month datetime = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        ServiceStartDate, ServiceScheduleId, Duration
	FROM            LessonReviewerVideo
	WHERE   (@ServiceScheduleId=-1 OR ServiceScheduleId = @ServiceScheduleId)
		AND	(@Month IS NULL OR 
				(MONTH(ServiceStartDate) = MONTH(@Month) AND YEAR(ServiceStartDate) = YEAR(@Month))
			)

	RETURN