
-- Procedure WebPageElement_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPageElement_Del]
	(
		@PageElementId int
	)
AS
	SET NOCOUNT ON
	
	if(@PageElementId > 0)
		BEGIN
			DELETE FROM WebPageElement
			WHERE PageElementId=@PageElementId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

