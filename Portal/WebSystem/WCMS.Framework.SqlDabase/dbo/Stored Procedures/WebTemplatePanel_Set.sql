CREATE PROCEDURE [dbo].[WebTemplatePanel_Set]
	(
		@TemplatePanelId int = -1,
		@Name nvarchar(256),
		@PanelName nvarchar(256),
		@Rank int,
		@ObjectId int,
		@RecordId int
	)
AS
	SET NOCOUNT ON
	
	if(@TemplatePanelId = -1)
		BEGIN
			-- Insert
			EXEC @TemplatePanelId = WebObject_NextId 'WebTemplatePanel';
			
			INSERT INTO WebTemplatePanel
			                         (Name, ObjectId, RecordId, PanelName, TemplatePanelId, Rank)
			VALUES        (@Name,@ObjectId, @RecordId,@PanelName,@TemplatePanelId, @Rank)
		END
	ELSE
		BEGIN
			-- Update
			UPDATE    WebTemplatePanel
			SET              Name = @Name, ObjectId = @ObjectId, RecordId=@RecordId, PanelName = @PanelName,
				Rank=@Rank
			WHERE     (TemplatePanelId = @TemplatePanelId)
		END
	
	SELECT @TemplatePanelId;
	
	RETURN