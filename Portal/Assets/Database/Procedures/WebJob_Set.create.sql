
-- Procedure WebJob_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebJob_Set
	(
		@Id int = -1,
		@Name nvarchar(250),
		@RecurrenceId int,
		@OccursEvery int,
		@Weekdays int,
		@ExecutionStartDate datetime,
		@ExecutionEndDate datetime,
		@ExecutionStatus int,
		@ExecutionMessage ntext,
		@Enabled int,
		@TypeName nvarchar(250),
		@StartDate datetime,
		@Description nvarchar(4000)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebJob
			SET              Name = @Name, RecurrenceId = @RecurrenceId, Weekdays = @Weekdays, OccursEvery = @OccursEvery, 
								  ExecutionStartDate = @ExecutionStartDate, ExecutionEndDate = @ExecutionEndDate, ExecutionStatus = @ExecutionStatus, 
								  ExecutionMessage = @ExecutionMessage, Enabled=@Enabled, TypeName=@TypeName, StartDate=@StartDate,
								  Description=@Description
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebJob'

			INSERT INTO WebJob
			                      (Name, RecurrenceId, Weekdays, OccursEvery, ExecutionStartDate, ExecutionEndDate, ExecutionStatus, 
								  ExecutionMessage, Id, Enabled, TypeName, StartDate, Description)
			VALUES     (@Name,@RecurrenceId,@Weekdays,@OccursEvery,@ExecutionStartDate,@ExecutionEndDate,@ExecutionStatus,
								@ExecutionMessage,@Id, @Enabled, @TypeName, @StartDate, @Description)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

