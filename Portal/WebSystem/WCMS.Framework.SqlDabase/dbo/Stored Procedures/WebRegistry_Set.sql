CREATE PROCEDURE [dbo].[WebRegistry_Set]
	(
		@RegistryId int = -1,
		@Key nvarchar(250),
		@Value ntext,
		@ParentId int,
		@StageId int
	)
AS
	SET NOCOUNT ON
	
	if(@RegistryId = -1)
		begin
			-- Insert
			EXEC @RegistryId = WebObject_NextId 'WebRegistry'
			
			INSERT INTO WebRegistry
			                         ([Key], Value, ParentId, RegistryId, StageId)
			VALUES        (@Key,@Value,@ParentId,@RegistryId, @StageId)
		end
	else
		begin
			-- Update
			
			UPDATE    WebRegistry
			SET              [Key] = @Key, Value = @Value, ParentId = @ParentId, StageId=@StageId
			WHERE     (RegistryId = @RegistryId)
		end
	
	SELECT @RegistryId
	
	RETURN