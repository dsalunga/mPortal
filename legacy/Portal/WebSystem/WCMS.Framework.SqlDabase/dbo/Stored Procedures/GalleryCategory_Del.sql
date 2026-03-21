CREATE PROCEDURE dbo.GalleryCategory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM GalleryCategory
		WHERE CategoryID=@Id
	
	RETURN