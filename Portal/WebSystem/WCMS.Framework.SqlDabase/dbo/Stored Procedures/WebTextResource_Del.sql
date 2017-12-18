CREATE PROCEDURE [dbo].[WebTextResource_Del]
	(
		@TextResourceId int
	)
AS
	SET NOCOUNT ON
	
	IF(@TextResourceId > 0)
		BEGIN
			DELETE FROM WebTextResource
			WHERE TextResourceId=@TextResourceId
		END
	
	RETURN