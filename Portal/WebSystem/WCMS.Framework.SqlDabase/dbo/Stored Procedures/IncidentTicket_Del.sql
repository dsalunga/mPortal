CREATE PROCEDURE dbo.IncidentTicket_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM IncidentTicket
		WHERE Id=@Id;

	RETURN