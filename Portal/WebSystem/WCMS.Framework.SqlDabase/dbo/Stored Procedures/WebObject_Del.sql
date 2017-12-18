CREATE PROCEDURE [dbo].[WebObject_Del]
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM WebObject
		WHERE Id=@Id
	
	RETURN