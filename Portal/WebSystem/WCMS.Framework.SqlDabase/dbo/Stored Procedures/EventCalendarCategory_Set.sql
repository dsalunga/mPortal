CREATE PROCEDURE dbo.EventCalendarCategory_Set
	(
		@Id int = -1,
		@Name nvarchar(250),
		@TemplateId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       EventCalendarCategories
			SET                Name = @Name, TemplateId = @TemplateId
			WHERE        (CategoryId = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'EventCalendarCategories'

			INSERT INTO EventCalendarCategories
			                         (Name, TemplateId, CategoryId)
			VALUES        (@Name,@TemplateId,@Id)
		END

	SELECT @Id

	RETURN