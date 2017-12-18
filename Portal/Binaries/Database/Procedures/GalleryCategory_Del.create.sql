
-- Procedure GalleryCategory_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.GalleryCategory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM GalleryCategory
		WHERE CategoryID=@Id
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

