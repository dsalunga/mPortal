
-- Procedure GalleryCategoryLink_GetTypeIdOut
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GalleryCategoryLink_GetTypeIdOut]
	(
		@ObjectId int,
		@RecordId int
	)
AS
	SET NOCOUNT ON
	
	SELECT     CategoryId, Title, Width, ImageURL, PhotoHeight, FolderName, PhotoWidth
	FROM         GalleryCategory
	WHERE     (CategoryId NOT IN
	                          (SELECT     GC.CategoryId
	                            FROM          GalleryCategory GC INNER JOIN
	                                                   GalleryCategoryLink GCL ON GC.CategoryId = GCL.CategoryId
	                            WHERE      (GCL.ObjectId = @ObjectId) AND (GCL.RecordID = @RecordId)))
	ORDER BY Title
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

