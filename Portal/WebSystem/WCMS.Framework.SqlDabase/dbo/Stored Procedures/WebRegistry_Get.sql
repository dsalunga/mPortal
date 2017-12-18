CREATE PROCEDURE [dbo].[WebRegistry_Get]
	(
		@RegistryId int = -1,
		@Key nvarchar(250) = null,
		@ParentId int = -2,
		@StageId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     RegistryId, [Key], Value, ParentId, StageId
	FROM         WebRegistry
	WHERE     (@RegistryId = - 1 OR
	                RegistryId = @RegistryId)
	      AND (@Key IS NULL OR
	                [Key] = @Key)
	      AND (@ParentId = -2 OR
					ParentId = @ParentId)
		  AND (@StageId = -2 OR
					StageId = @StageId)
	ORDER BY ParentId, [Key]
	
	RETURN