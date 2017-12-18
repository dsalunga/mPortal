
-- Procedure WebPartControl_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartControl_Get]
	(
		@PartControlId int = -1,
		@PartId int = -1,
		@Identity nvarchar(256) = NULL,
		@ParentId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     PartControlId, PartId, Name, [Identity], ConfigFileName, PartAdminId,
			EntryPoint, ParentId
	FROM         WebPartControl
	WHERE     
			(@PartId = -1 OR PartId=@PartId)
		AND (@PartControlId = - 1 OR PartControlId = @PartControlId)
		AND (@Identity IS NULL OR [Identity]=@Identity)
		AND (@ParentId =-1 OR ParentId=@ParentId)
	ORDER BY Name
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

