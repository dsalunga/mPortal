CREATE PROCEDURE dbo.WebTheme_Get
	(
		@Id int = -1,
		@TemplateId int = -2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, TemplateId, ParentId, [Identity], SkinId
	FROM         WebTheme
	WHERE     (@Id = -1 OR Id = @Id)
		AND (@TemplateId=-2 OR TemplateId=@TemplateId)
	ORDER BY [Name]

	RETURN