
-- Procedure WebTextResource_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebTextResource_Get]
	(
		@TextResourceId int = -1,
		@DirectoryId int = -2,
		@ContentTypeId int = -2,
		@Title nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT     TextResourceId, ContentTypeId, Title, Content, DirectoryId, Rank, DateModified, DatePersisted, PhysicalPath
	FROM         WebTextResource
	WHERE     (@TextResourceId < 1 OR TextResourceId = @TextResourceId)
			AND (@Title IS NULL OR Title=@Title)
	      AND (@DirectoryId = -2 OR DirectoryId = @DirectoryId)
		  AND (@ContentTypeId = -2 OR ContentTypeId=@ContentTypeId)
	ORDER BY Rank
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

