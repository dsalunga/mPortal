CREATE PROCEDURE dbo.LessonReviewerVideo_Del
	(
		@ServiceScheduleId int
	)
AS
	SET NOCOUNT ON

	IF(@ServiceScheduleId > 0)
		DELETE FROM LessonReviewerVideo
		WHERE ServiceScheduleId=@ServiceScheduleId;

	RETURN