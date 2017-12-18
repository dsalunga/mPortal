CREATE PROCEDURE [dbo].[WebPageElement_Del]
	(
		@PageElementId int
	)
AS
	SET NOCOUNT ON
	
	if(@PageElementId > 0)
		BEGIN
			DELETE FROM WebPageElement
			WHERE PageElementId=@PageElementId
		END
	
	RETURN