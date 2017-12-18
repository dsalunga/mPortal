CREATE PROCEDURE dbo.IncidentTicketHistory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM IncidentTicketHistory
		WHERE Id=@Id;

	RETURN