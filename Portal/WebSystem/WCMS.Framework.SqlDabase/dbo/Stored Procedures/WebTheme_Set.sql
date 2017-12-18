CREATE PROCEDURE dbo.WebTheme_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@TemplateId int,
		@ParentId int,
		@Identity nvarchar(500),
		@SkinId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebTheme
			SET              Name = @Name, TemplateId = @TemplateId, ParentId=@ParentId,
				[Identity]=@Identity, SkinId=@SkinId
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebTheme';

			INSERT INTO WebTheme
			                      (Name, TemplateId, Id, ParentId, [Identity], SkinId)
			VALUES     (@Name,@TemplateId,@Id, @ParentId, @Identity, @SkinId)
		END

	SELECT @Id;

	RETURN