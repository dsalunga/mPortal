CREATE PROCEDURE dbo.Sportsfest_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM Sportsfest
		WHERE Id=@Id;

	RETURN