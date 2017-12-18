CREATE PROCEDURE dbo.EventCalendarTemplate_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM EventCalendarTemplate
		WHERE TemplateId=@Id;

	RETURN