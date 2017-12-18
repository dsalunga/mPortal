CREATE PROCEDURE [dbo].[ArticleColumn_Get]
	(
		@ColumnId int = -1,
		@TemplateId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     ColumnId, Name, TemplateId, Id, IsSingle
	FROM         ArticleColumn
	WHERE
		(@ColumnId = -1 OR ColumnId=@ColumnId)
		AND
		(@TemplateId=-1 OR TemplateId=@TemplateId)
	
	RETURN