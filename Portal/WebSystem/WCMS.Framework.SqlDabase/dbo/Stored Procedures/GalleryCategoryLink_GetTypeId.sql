CREATE PROCEDURE [dbo].[GalleryCategoryLink_GetTypeId]
	(
		@ObjectId int,
		@RecordId int,
		@SortBy nvarchar(50) = 'Title'
	)
AS
	SET NOCOUNT ON
	
	SELECT     GCL.Id, GC.CategoryId, GC.Title, GC.ImageURL, GC.Width, 
				GC.PhotoHeight, GC.FolderName, GC.PhotoWidth
	FROM         GalleryCategory AS GC INNER JOIN
	                      GalleryCategoryLink AS GCL ON GC.CategoryId = GCL.CategoryId
	WHERE     (GCL.ObjectId = @ObjectId) AND (GCL.RecordId = @RecordId)
	ORDER BY
		CASE WHEN @SortBy = 'Date' THEN GC.Title END,
		CASE WHEN @SortBy = 'Title' THEN GC.DateModified END DESC;
	
	RETURN