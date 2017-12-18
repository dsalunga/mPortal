CREATE PROCEDURE [GalleryObject_Get]
	(
		@RecordId int,
		@ObjectId int
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) Id, InitialControl, ThumbColumns, ThumbRows, AlbumColumns, AlbumCellPadding,
				MaxPhotoWidth
	FROM         GalleryObject
	WHERE     (ObjectId = @ObjectId) AND (RecordId = @RecordId)
	
	RETURN