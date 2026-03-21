CREATE PROCEDURE dbo.WebFolder_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			DELETE FROM WebFolder 
			WHERE Id=@Id
		END
	
	RETURN