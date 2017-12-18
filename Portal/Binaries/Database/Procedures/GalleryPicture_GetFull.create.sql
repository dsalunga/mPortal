
-- Procedure GalleryPicture_GetFull
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GalleryPicture_GetFull]
	(
		@GalleryId INT = -1,
		@CategoryId INT
	)
AS
	SET NOCOUNT ON
	
	SELECT		p.GalleryId, p.Caption, p.Thumbnail, p.ImageURL, 
				pc.Title AS CategoryName, pc.CategoryId
	FROM         Gallery p 
		INNER JOIN GalleryCategory pc ON p.CategoryId = pc.CategoryId
	WHERE     (p.IsActive = 1)
				AND (p.CategoryId = @CategoryId)
				AND (@GalleryId = -1 OR p.GalleryId = @GalleryId)
						
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

