
-- Procedure WebPart_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPart_Get]
	(
		@PartId int = -1,
		@Identity nvarchar(250) = NULL,
		@Active int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     PartId, Name, [Identity], Active
	FROM         WebPart
	WHERE     
			(@PartId = -1 OR PartId = @PartId)
		AND	(@Identity IS NULL OR [Identity]=@Identity)
		AND (@Active = -1 OR Active=@Active)
	ORDER BY Name
	
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

