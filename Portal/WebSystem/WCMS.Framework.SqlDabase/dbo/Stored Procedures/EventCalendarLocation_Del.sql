CREATE PROCEDURE dbo.EventCalendarLocation_Del
	(
		@LocationId int
	)
AS
	SET NOCOUNT ON

	IF(@LocationId > 0)
		DELETE FROM EventCalendarLocations
		WHERE LocationId = @LocationId;

	RETURN