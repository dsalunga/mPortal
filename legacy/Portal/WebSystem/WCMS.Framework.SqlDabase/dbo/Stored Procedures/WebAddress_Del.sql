CREATE PROCEDURE dbo.WebAddress_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebAddress
		WHERE Id=@Id

	RETURN