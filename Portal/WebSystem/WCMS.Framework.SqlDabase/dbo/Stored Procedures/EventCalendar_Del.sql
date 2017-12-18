CREATE PROCEDURE dbo.EventCalendar_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM EventCalendar
		WHERE Id=@Id;

	RETURN