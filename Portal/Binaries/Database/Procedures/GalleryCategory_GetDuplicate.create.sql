
-- Procedure GalleryCategory_GetDuplicate
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

