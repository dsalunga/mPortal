CREATE PROCEDURE [Gallery_Get]
(
	@CategoryID int = -1,
	@GalleryID int = -1
)
AS
	SET NOCOUNT ON
	
	if(@GalleryID > 0)
		begin
			SELECT     GalleryID, Caption, Thumbnail, ImageURL, DateCreated, SiteID, IsActive, CategoryID
			FROM         Gallery
			WHERE     (GalleryID = @GalleryID)
		end
	else
		begin
			SELECT     G.GalleryID, G.Caption, G.DateCreated, G.IsActive, G.ImageURL, G.SiteId, G.CategoryID, C.Title
			FROM         Gallery AS G
				INNER JOIN GalleryCategory C
					ON G.CategoryID=C.CategoryID
			WHERE     (@CategoryID = -1 OR G.CategoryID = @CategoryID)
			ORDER BY C.Title, G.Caption
		end
	
	RETURN