CREATE PROCEDURE [dbo].[WebGroup_Del]
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			DELETE FROM WebGroup
			WHERE Id=@Id
		END
	
	RETURN