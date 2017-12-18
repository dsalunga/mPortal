﻿CREATE PROCEDURE dbo.WebJob_Get
	(
		@Id int = -1,
		@Name nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, RecurrenceId, Weekdays, OccursEvery, ExecutionStartDate, ExecutionEndDate, ExecutionStatus, 
				ExecutionMessage, Enabled, TypeName, StartDate, Description
	FROM         WebJob
	WHERE
		(@Id=-1 OR Id=@Id)
		AND (@Name IS NULL OR Name=@Name)

	RETURN