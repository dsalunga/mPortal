CREATE PROCEDURE dbo.WebParameter_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebParameter
		WHERE Id=@Id;

	RETURN