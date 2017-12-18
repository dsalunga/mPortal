CREATE PROCEDURE [dbo].[WebTemplate_Set]
	(
		@Id int = -1,
		@Name nvarchar(256),
		@FileName nvarchar(256),
		@Identity nvarchar(256),
		@PrimaryPanelId int = -1,
		@Version int = 0,
		@VersionOf int = -1,
		@Content ntext,
		@DateModified datetime = NULL,
		@ThemeId int,
		@SkinId int,
		@Standalone int,
		@ParentId int,
		@TemplateEngineId int
	)
AS
	SET NOCOUNT ON
	
	IF(@DateModified IS NULL)
		SET @DateModified = GETDATE();
	
	
	if(@Id = -1)
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'WebTemplate';
			
			INSERT INTO WebTemplate
			            (Name, FileName, [Identity], PrimaryPanelId, Id, Content, DateModified,
							ThemeId, Standalone, ParentId, SkinId, TemplateEngineId)
			VALUES      (@Name,@FileName,@Identity,@PrimaryPanelId, @Id, @Content, @DateModified,
							@ThemeId, @Standalone, @ParentId, @SkinId, @TemplateEngineId);
		END
	ELSE
		BEGIN
			-- Update
			UPDATE    WebTemplate
			SET              Name = @Name, FileName = @FileName, [Identity] = @Identity, PrimaryPanelId = @PrimaryPanelId,
							Version=@Version, VersionOf=@VersionOf, Content=@Content, DateModified = @DateModified,
							ThemeId=@ThemeId, Standalone=@Standalone, ParentId=@ParentId, SkinId=@SkinId,
							TemplateEngineId=@TemplateEngineId
			WHERE     (Id = @Id);
		END
	
	SELECT @Id;
	
	RETURN