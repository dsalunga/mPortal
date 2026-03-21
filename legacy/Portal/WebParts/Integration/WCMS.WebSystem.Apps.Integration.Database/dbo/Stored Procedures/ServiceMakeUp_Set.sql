CREATE PROCEDURE dbo.LessonReviewerSession_Set
	(
		@Id int = -1,
		@ServiceScheduleID int,
		@ServiceStartDate datetime,
		@ServiceName nvarchar(150),
		@DateStarted datetime,
		@DateCompleted datetime,
		@MemberId int,
		@AbsentReason nvarchar(4000),
		@CouncillorNotes nvarchar(4000),
		@CouncillorUserId int,
		@Status int,
		@DateApproved datetime,
		@AdditionalNotes nvarchar(4000)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    LessonReviewerSession
			SET              ServiceScheduleID = @ServiceScheduleID, ServiceStartDate = @ServiceStartDate, ServiceName = @ServiceName, DateStarted = @DateStarted, 
			                      DateCompleted = @DateCompleted, MemberId = @MemberId, AbsentReason = @AbsentReason, CouncillorNotes = @CouncillorNotes, 
			                      CouncillorUserId = @CouncillorUserId, Status = @Status, DateApproved = @DateApproved, AdditionalNotes=@AdditionalNotes
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'LessonReviewerSession';

			INSERT INTO LessonReviewerSession
								  (Id, ServiceScheduleID, ServiceStartDate, ServiceName, DateStarted, DateCompleted, MemberId, 
								  AbsentReason, CouncillorNotes, CouncillorUserId, Status, DateApproved, AdditionalNotes)
			VALUES     (@Id,@ServiceScheduleID,@ServiceStartDate,@ServiceName,@DateStarted,@DateCompleted,@MemberId,
						@AbsentReason,@CouncillorNotes,@CouncillorUserId,@Status,@DateApproved, @AdditionalNotes)
		END

	SELECT @Id

	RETURN