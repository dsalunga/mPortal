
-- Procedure WebFile_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebFile_Del
	(
		@FileId int
	)
AS
	SET NOCOUNT ON
	
	IF(@FileId > 0)
		BEGIN
			DELETE FROM WebFile
			WHERE FileId=@FileId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

