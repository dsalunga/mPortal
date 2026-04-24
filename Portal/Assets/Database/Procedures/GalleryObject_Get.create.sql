
-- Procedure GalleryObject_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

