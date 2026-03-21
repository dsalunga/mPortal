CREATE PROCEDURE [dbo].[EventCalendarCategories_Get]
	(
		@CategoryId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     CategoryId, Name, TemplateId
	FROM         EventCalendarCategories
	WHERE
			(@CategoryId = -1 OR
				CategoryId = @CategoryId)
	
	RETURN