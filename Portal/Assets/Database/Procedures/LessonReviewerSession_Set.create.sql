
-- Procedure LessonReviewerSession_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LessonReviewerSession_Set]
	(
		@Id int = -1,
		@ServiceScheduleID int,
		@ServiceStartDate datetime,
		@ServiceName nvarchar(150),
		@DateStarted datetime,
		@DateCompleted datetime,
		@MemberId int,
		@AbsentReason nvarchar(MAX),
		@WorkerNotes nvarchar(MAX),
		@WorkerUserId int,
		@Status int,
		@DateApproved datetime,
		@AdditionalNotes nvarchar(MAX),
		@AttendanceType int,
		@PageId int,
		@Extra nvarchar(MAX)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    LessonReviewerSession
			SET              ServiceScheduleID = @ServiceScheduleID, ServiceStartDate = @ServiceStartDate, ServiceName = @ServiceName, DateStarted = @DateStarted, 
			                      DateCompleted = @DateCompleted, MemberId = @MemberId, AbsentReason = @AbsentReason, WorkerNotes = @WorkerNotes, 
			                      WorkerUserId = @WorkerUserId, Status = @Status, DateApproved = @DateApproved, AdditionalNotes=@AdditionalNotes,
								  AttendanceType=@AttendanceType, PageId=@PageId, Extra=@Extra
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'LessonReviewerSession';

			INSERT INTO LessonReviewerSession
								  (Id, ServiceScheduleID, ServiceStartDate, ServiceName, DateStarted, DateCompleted, MemberId, 
								  AbsentReason, WorkerNotes, WorkerUserId, Status, DateApproved, AdditionalNotes, AttendanceType, PageId, Extra)
			VALUES     (@Id,@ServiceScheduleID,@ServiceStartDate,@ServiceName,@DateStarted,@DateCompleted,@MemberId,
						@AbsentReason,@WorkerNotes,@WorkerUserId,@Status,@DateApproved, @AdditionalNotes, @AttendanceType, @PageId, @Extra)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

