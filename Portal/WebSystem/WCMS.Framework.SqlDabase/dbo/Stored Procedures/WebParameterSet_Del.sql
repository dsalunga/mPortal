CREATE PROCEDURE dbo.WebParameterSet_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebParameterSet
		WHERE Id=@Id;

	RETURN