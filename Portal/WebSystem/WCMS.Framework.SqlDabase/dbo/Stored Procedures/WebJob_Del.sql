CREATE PROCEDURE dbo.WebJob_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebJob
		WHERE Id=@Id

	RETURN