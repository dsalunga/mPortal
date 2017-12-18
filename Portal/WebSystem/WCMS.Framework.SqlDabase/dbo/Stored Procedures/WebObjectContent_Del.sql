CREATE PROCEDURE [dbo].[WebObjectContent_Del]
	(
		@ObjectContentId int
	)
AS
	SET NOCOUNT ON
	
	if(@ObjectContentId > 0)
		BEGIN
			DELETE FROM WebObjectContent
			WHERE ObjectContentId=@ObjectContentId
		END
	
	RETURN