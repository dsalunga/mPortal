CREATE PROCEDURE [dbo].[WebContent_Del]
	(
		@ContentId int
	)
AS
	SET NOCOUNT ON
	
	DELETE FROM WebContent
	WHERE ContentId = @ContentId
	
	RETURN