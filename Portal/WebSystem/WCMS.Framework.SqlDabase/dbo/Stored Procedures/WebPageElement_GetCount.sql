CREATE PROCEDURE [dbo].[WebPageElement_GetCount]
	(
		@RecordId int = -1,
		@ObjectId int = -1,
		@TemplatePanelId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT COUNT(*) 
	FROM WebPageElement
	WHERE	
			(@RecordId = -1 OR RecordId = @RecordId)
		AND (@TemplatePanelId = -1 OR TemplatePanelId = @TemplatePanelId)
		AND (@ObjectId = -1 OR ObjectId = @ObjectId)
	
	RETURN