CREATE PROCEDURE [dbo].[WebPagePanel_Set]
	(
		@PagePanelId int = -1,
		@TemplatePanelId int,
		@PageId int,
		@UsageTypeId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PagePanelId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPagePanel
			SET              TemplatePanelId = @TemplatePanelId, PageId = @PageID, UsageTypeId = @UsageTypeId
			WHERE     (PagePanelId = @PagePanelId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @PagePanelId = WebObjects_NextId 'WebPagePanel'
			
			INSERT INTO WebPagePanel
			                      (TemplatePanelId, PageId, UsageTypeId, PagePanelId)
			VALUES     (@TemplatePanelId,@PageID,@UsageTypeId,@PagePanelId)
		END
	
	SELECT @PagePanelId
	
	RETURN