CREATE PROCEDURE dbo.LessonReviewerSession_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id>0)
		DELETE FROM LessonReviewerSession
		WHERE Id=@Id;

	RETURN