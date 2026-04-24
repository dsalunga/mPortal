
-- Procedure WebTextResource_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebTextResource_Del]
	(
		@TextResourceId int
	)
AS
	SET NOCOUNT ON
	
	IF(@TextResourceId > 0)
		BEGIN
			DELETE FROM WebTextResource
			WHERE TextResourceId=@TextResourceId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

