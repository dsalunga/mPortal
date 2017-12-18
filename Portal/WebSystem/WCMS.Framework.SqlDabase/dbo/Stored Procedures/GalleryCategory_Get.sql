CREATE PROCEDURE [dbo].[GalleryCategory_Get]
	(
		@CategoryID int = -1,
		@ObjectId int = -1,
		@RecordId int = -1,
		@Title nvarchar(250) = NULL,
		@SortBy nvarchar(50) = 'Title'
	)
AS
	SET NOCOUNT ON
	
	IF(@ObjectId > 0 AND @RecordId > 0)
		BEGIN
			SELECT     LINK.Id, ALBUM.CategoryId, ALBUM.Title, ALBUM.ImageURL, ALBUM.Width, 
						ALBUM.PhotoHeight, ALBUM.FolderName, ALBUM.PhotoWidth, ALBUM.DateModified
			FROM         GalleryCategory AS ALBUM INNER JOIN
								  GalleryCategoryLink AS LINK ON ALBUM.CategoryId = LINK.CategoryId
			WHERE     (LINK.ObjectId = @ObjectId) AND (LINK.RecordId = @RecordId)
			ORDER BY
				CASE WHEN @SortBy = 'Date' THEN ALBUM.Title END,
				CASE WHEN @SortBy = 'Title' THEN ALBUM.DateModified END DESC;
		END
	ELSE
		BEGIN
			SELECT        Title, ImageURL, CategoryID, Width, PhotoHeight, FolderName, PhotoWidth, DateModified
			FROM            GalleryCategory
			WHERE        (@CategoryID = - 1 OR CategoryID = @CategoryID)
					AND (@Title IS NULL OR Title=@Title)
			ORDER BY
				CASE WHEN @SortBy = 'Date' THEN Title END,
				CASE WHEN @SortBy = 'Title' THEN DateModified END DESC;
		END

	RETURN