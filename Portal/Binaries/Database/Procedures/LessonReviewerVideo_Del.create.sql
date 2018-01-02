
-- Procedure LessonReviewerVideo_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

