
-- Procedure LessonReviewerSession_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.LessonReviewerSession_Get
	(
		@Id int = -1,
		@MemberId int = -1,
		@Status int = -1,
		@DateStart datetime = NULL,
		@DateEnd datetime = NULL,
		@ServiceScheduleId int = -1,
		@AttendanceType int = -1,
		@PageId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, ServiceScheduleID, ServiceStartDate, ServiceName, DateStarted, DateCompleted, MemberId, AbsentReason, WorkerNotes, WorkerUserId, Status, 
						  DateApproved, AdditionalNotes, AttendanceType, PageId, Extra
	FROM         LessonReviewerSession
	WHERE     (@Id=-1 OR Id = @Id)
		AND	(@Status=-1 OR Status=@Status)
		AND	(@MemberId=-1 OR MemberId=@MemberId)
		AND (@ServiceScheduleId = -1 OR ServiceScheduleID=@ServiceScheduleId)
		AND	(@DateStart IS NULL OR @DateEnd IS NULL OR
				(ServiceStartDate >= @DateStart AND ServiceStartDate <= @DateEnd))
		AND (@AttendanceType = -1 OR AttendanceType=@AttendanceType)
		AND (@PageId = -1 OR PageId=@PageId)
	ORDER BY DateStarted DESC

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

