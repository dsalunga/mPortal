
-- Procedure LessonReviewerVideo_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.LessonReviewerVideo_Set
	(
		@ServiceScheduleId int,
		@ServiceStartDate datetime,
		@Duration int
	)
AS
	SET NOCOUNT ON

	IF EXISTS (SELECT ServiceScheduleId FROM LessonReviewerVideo WHERE ServiceScheduleId=@ServiceScheduleId)
		BEGIN
			-- Update

			UPDATE       LessonReviewerVideo
			SET                ServiceStartDate = @ServiceStartDate, Duration = @Duration
			WHERE        (ServiceScheduleId = @ServiceScheduleId)
		END
	ELSE
		BEGIN
			-- Insert

			INSERT INTO LessonReviewerVideo
									 (ServiceStartDate, ServiceScheduleId, Duration)
			VALUES        (@ServiceStartDate,@ServiceScheduleId,@Duration)
		END

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

