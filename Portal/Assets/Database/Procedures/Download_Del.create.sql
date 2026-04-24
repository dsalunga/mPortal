
-- Procedure Download_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Download_Del] 
	(
		@DownloadID int
	)
AS
	SET NOCOUNT ON
	
	DECLARE @Filename nvarchar(255)
	
	SET @Filename = (SELECT Filename FROM Download WHERE DownloadID=@DownloadID)
	DELETE FROM Download WHERE DownloadID=@DownloadID
	
	SELECT @Filename AS Filename
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

