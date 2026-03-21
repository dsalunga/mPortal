CREATE PROCEDURE dbo.MCCandidate_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MCCandidate
		WHERE Id=@Id;

	RETURN