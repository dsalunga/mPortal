
-- Procedure WebMasterPage_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebMasterPage_Del]
	(
		@MasterPageId int
	)
AS
	SET NOCOUNT ON
	
	if(@MasterPageId > 0)
		BEGIN
			
			DELETE FROM WebMasterPage
			WHERE MasterPageId=@MasterPageId
			
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

