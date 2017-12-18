
-- Procedure WebShortUrl_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebShortUrl_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@PageId int,
		@PageUrl nvarchar(max)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update
			UPDATE       WebShortUrl
			SET                Name = @Name, PageId = @PageId, PageUrl=@PageUrl
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			INSERT INTO WebShortUrl
			                         (Name, PageId, PageUrl)
			VALUES        (@Name,@PageId, @PageUrl)

			SET @Id = CAST(SCOPE_IDENTITY() AS INT);
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

