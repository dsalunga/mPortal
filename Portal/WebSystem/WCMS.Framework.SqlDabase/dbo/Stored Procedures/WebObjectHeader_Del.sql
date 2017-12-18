CREATE PROCEDURE [dbo].[WebObjectHeader_Del]
	(
		@ObjectHeaderId int
	)
AS
	SET NOCOUNT ON
	
	IF(@ObjectHeaderId > 0)
		BEGIN
			DELETE FROM WebObjectHeader
			WHERE ObjectHeaderId = @ObjectHeaderId
		END
	
	RETURN