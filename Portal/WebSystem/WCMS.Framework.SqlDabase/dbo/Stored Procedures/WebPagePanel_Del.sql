CREATE PROCEDURE [dbo].[WebPagePanel_Del]
	(
		@PagePanelId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PagePanelId > 0)
		DELETE FROM WebPagePanel
		WHERE PagePanelId=@PagePanelId
	
	RETURN