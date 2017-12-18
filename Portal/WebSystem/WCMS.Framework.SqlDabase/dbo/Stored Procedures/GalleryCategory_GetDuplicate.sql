CREATE PROCEDURE [GalleryCategory_GetDuplicate]
	(
		@CategoryID int,
		@Title nvarchar(256)
	)
AS
	SET NOCOUNT ON
	
	SELECT     CategoryID
	FROM         GalleryCategory
	WHERE     (Title = @Title) AND (CategoryID <> @CategoryID)
	RETURN