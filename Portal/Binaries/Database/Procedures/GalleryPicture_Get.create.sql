
-- Procedure GalleryPicture_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GalleryPicture_Get]
	(
		@GalleryID INT = -1,
		@RecordId INT = -1,
		@ObjectId int = -1,
		@CategoryID INT = -1
	)
AS
	SET NOCOUNT ON
	
	IF(@GalleryID = -1)
		BEGIN
			SELECT     p.GalleryID, p.Caption, p.Thumbnail, p.ImageURL
			FROM         GalleryCategoryLink sp INNER JOIN
			                      Gallery p ON sp.CategoryID = p.CategoryID
			WHERE     (p.IsActive = 1) 
						AND (sp.RecordId = @RecordId) 
						AND (sp.ObjectId = @ObjectId) 
						AND (@CategoryID = -1 OR p.CategoryID = @CategoryID)
						
			/*
			SELECT     p.GalleryID, p.Caption, p.Thumbnail, p.ImageURL
			FROM         GalleryLocation sp INNER JOIN
			                      Gallery p ON sp.GalleryID = p.GalleryID
			WHERE     (p.IsActive = 1) 
						AND (sp.RecordId = @RecordId) 
						AND (sp.ObjectId = @ObjectId) 
						AND (@CategoryID = -1 OR p.CategoryID = @CategoryID)
			*/
		END
	ELSE
		BEGIN
			SELECT     p.Caption, pc.Title AS CategoryName, pc.CategoryID, p.ImageURL
			FROM         Gallery p INNER JOIN
			                      GalleryCategory pc ON p.CategoryID = pc.CategoryID
			WHERE     (p.IsActive = 1) AND (p.GalleryID = @GalleryID)
		END
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

