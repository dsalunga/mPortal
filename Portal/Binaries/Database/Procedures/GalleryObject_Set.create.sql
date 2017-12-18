
-- Procedure GalleryObject_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GalleryObject_Set]
	(
		@RecordId int,
		@ObjectId int,
	
		@InitialControl nvarchar(256) = null,
		@ThumbColumns int = 4,
		@ThumbRows int = 5,
		@AlbumColumns int = 2,
		@AlbumCellPadding int = 15,
		@MaxPhotoWidth int = 700
	)
AS
	SET NOCOUNT ON
	DECLARE @Id int
	
	SET @Id = (SELECT     TOP (1) Id
	                          FROM         GalleryObject
	                          WHERE     (ObjectId = @ObjectId) AND (RecordId = @RecordId))
	
	if(@Id is null)
		begin
			/* INSERT */
			INSERT INTO GalleryObject
						(RecordId, ObjectId, InitialControl, ThumbColumns, ThumbRows, AlbumColumns, AlbumCellPadding, 
							MaxPhotoWidth)
			VALUES		(@RecordId,@ObjectId,@InitialControl,@ThumbColumns,@ThumbRows,@AlbumColumns,@AlbumCellPadding,
							@MaxPhotoWidth)
		end
	else
		begin
			/* UPDATE */
			UPDATE    GalleryObject
			SET			InitialControl = @InitialControl, ThumbColumns = @ThumbColumns, ThumbRows = @ThumbRows, AlbumColumns = @AlbumColumns, 
							AlbumCellPadding = @AlbumCellPadding, MaxPhotoWidth=@MaxPhotoWidth
			WHERE     (Id = @Id)
		end
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

