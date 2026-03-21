CREATE PROCEDURE [dbo].[Articles_Del]
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM Articles 
		WHERE Id=@Id
	
	RETURN