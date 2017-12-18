CREATE PROCEDURE dbo.IncidentCategory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM IncidentCategory
		WHERE Id=@Id;

	RETURN