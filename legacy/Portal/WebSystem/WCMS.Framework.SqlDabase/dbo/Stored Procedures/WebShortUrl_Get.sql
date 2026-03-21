CREATE PROCEDURE dbo.WebShortUrl_Get
	(
		@Id int = -1,
		@PageId int = -1,
		@Name nvarchar(500) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name, PageId
	FROM            WebShortUrl
	WHERE        (@Id=-1 OR Id = @Id)
			AND (@PageId = -1 OR PageId=@PageId)
			AND (@Name IS NULL OR Name=@Name)

	RETURN