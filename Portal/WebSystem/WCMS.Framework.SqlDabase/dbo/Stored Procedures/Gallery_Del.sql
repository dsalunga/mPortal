CREATE PROCEDURE dbo.Gallery_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM Gallery
		WHERE GalleryID=@Id
	
	RETURN