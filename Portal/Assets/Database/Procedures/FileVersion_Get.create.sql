
-- Procedure FileVersion_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.FileVersion_Get
	(
		@Id int = -1,
		@FileId int = -1,
		@Activity int = -1,
		@UserId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, FileId, VersionDate, Activity, UserId
	FROM         FileVersion
	WHERE     (@Id = -1 OR Id = @Id) 
		AND (@FileId =-1 OR FileId = @FileId) 
		AND (@Activity =-1 OR Activity = @Activity) 
		AND (@UserId =-1 OR UserId = @UserId)
	ORDER BY VersionDate DESC

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

