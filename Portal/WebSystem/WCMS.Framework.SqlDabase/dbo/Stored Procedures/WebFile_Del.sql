CREATE PROCEDURE dbo.WebFile_Del
	(
		@FileId int
	)
AS
	SET NOCOUNT ON
	
	IF(@FileId > 0)
		BEGIN
			DELETE FROM WebFile
			WHERE FileId=@FileId
		END
	
	RETURN