CREATE PROCEDURE dbo.MChapter_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM MChapter
		WHERE Id= @Id;
	
	RETURN