CREATE PROCEDURE [dbo].[EventCalendarEvents_Del]
	(
		@EventId int
	)
AS
	SET NOCOUNT ON
	
	IF(@EventId > 0)
		BEGIN
			DELETE FROM EventCalendarEvents
			WHERE EventId=@EventId
		END
	
	RETURN