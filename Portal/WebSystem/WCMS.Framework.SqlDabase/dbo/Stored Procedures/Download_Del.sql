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