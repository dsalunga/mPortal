CREATE PROCEDURE [dbo].[WebPage_Del]
	(
		@PageId int
	)
AS
	SET NOCOUNT ON
	
	if(@PageId > 0)
		BEGIN
			DELETE FROM WebPage
			WHERE PageId=@PageId
		END
	
	RETURN