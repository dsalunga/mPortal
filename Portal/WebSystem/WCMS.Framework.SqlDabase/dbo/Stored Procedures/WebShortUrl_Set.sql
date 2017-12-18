CREATE PROCEDURE dbo.WebShortUrl_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@PageId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       WebShortUrl
			SET                Name = @Name, PageId = @PageId
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			INSERT INTO WebShortUrl
			                         (Name, PageId)
			VALUES        (@Name,@PageId)

			SET @Id = CAST(SCOPE_IDENTITY() AS INT);
		END

	SELECT @Id;

	RETURN