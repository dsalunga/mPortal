CREATE PROCEDURE dbo.EventCalendarCategory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM EventCalendarCategories
		WHERE CategoryId=@Id;
	
	RETURN