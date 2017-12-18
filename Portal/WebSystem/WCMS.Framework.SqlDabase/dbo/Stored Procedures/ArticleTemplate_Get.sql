CREATE PROCEDURE [dbo].[ArticleTemplate_Get]
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, Date, [File], ImageUrl, ListItemTemplate, ListTemplate, DetailsTemplate, DateFormat
	FROM         ArticleTemplate
	WHERE
		(@Id=-1 OR Id=@Id)
	
	RETURN