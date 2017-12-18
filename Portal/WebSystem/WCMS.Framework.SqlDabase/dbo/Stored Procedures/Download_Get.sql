CREATE PROCEDURE [Download_Get] 
	(
		@DownloadID int = null
	)
AS
	SET NOCOUNT ON
	
	if(@DownloadID is not null)
		begin
			SELECT * FROM Download WHERE DownloadID=@DownloadID
		end
	else
		begin
			SELECT     DownloadID, FileDate, DateModified, Description, Filename, UserID, Rank, Name
			FROM         Download
			ORDER BY FileDate DESC, Rank, Name
		end
	
	RETURN