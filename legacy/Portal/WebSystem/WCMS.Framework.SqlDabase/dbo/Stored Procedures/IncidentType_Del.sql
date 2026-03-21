CREATE PROCEDURE dbo.IncidentType_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM IncidentType
		WHERE Id=@Id;

	RETURN