CREATE PROCEDURE dbo.Job_Get
	(
		@Id int = -1,
		@Keyword nvarchar(1000) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Title, Description
	FROM            Job
	WHERE        (@Id=-1 OR Id = @Id)
		AND (@Keyword IS NULL OR Title LIKE '%' + @Keyword + '%')

	RETURN