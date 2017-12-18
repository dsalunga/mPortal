
-- Procedure ArticleTemplate_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.ArticleTemplate_Set
	(
		@Id int = -1,
		@Name nvarchar(250),
		@Date datetime,
		@File nvarchar(250),
		@ImageUrl nvarchar(250),
		@ListItemTemplate nvarchar(2500),
		@ListTemplate nvarchar(2500),
		@DetailsTemplate nvarchar(2500),
		@DateFormat nvarchar(500)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    ArticleTemplate
			SET              Name = @Name, Date = @Date, [File] = @File, ImageUrl = @ImageUrl, ListTemplate = @ListTemplate, ListItemTemplate = @ListItemTemplate, 
								  DetailsTemplate = @DetailsTemplate, DateFormat=@DateFormat
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'ArticleTemplate';

			INSERT INTO ArticleTemplate
			                      (Name, Date, [File], ImageUrl, ListTemplate, ListItemTemplate, DetailsTemplate, Id,
								  DateFormat)
			VALUES     (@Name,@Date,@File,@ImageUrl,@ListTemplate,@ListItemTemplate,@DetailsTemplate,@Id, @DateFormat)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

