CREATE PROCEDURE [dbo].[WebPageElement_Get]
	(
		@PageElementId int = -1,
		@RecordId int = -1,
		@ObjectId int = -1,
		@TemplatePanelId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     PageElementId, RecordId, Name, TemplatePanelId, Rank, PartControlTemplateId, Active, 
				ObjectId, UsePartTemplatePath, PublicAccess, ManagementAccess
	FROM         WebPageElement
	WHERE     (@PageElementId < 1 OR
	                      PageElementId = @PageElementId) 
	                      
	AND (@RecordId < 1 OR
	                      RecordId = @RecordId)
	AND (@ObjectId < 1 OR
				ObjectId=@ObjectId)
				
	AND (@TemplatePanelId < 1 OR
		TemplatePanelId=@TemplatePanelId)
	
	ORDER BY 
		Rank
	
	RETURN