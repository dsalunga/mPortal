﻿CREATE PROCEDURE [dbo].[WebPagePanel_Get]
	(
		@PagePanelId int = -1,
		@TemplatePanelId int = -1,
		@PageId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     PagePanelId, TemplatePanelId, PageId, UsageTypeId
	FROM         WebPagePanel
	WHERE	(@PagePanelId = -1 OR
				PagePanelId = @PagePanelId)
		AND	(@TemplatePanelId = -1 OR
				TemplatePanelId = @TemplatePanelId)
		AND (@PageId = -1 OR
				PageId = @PageId)
	
	RETURN