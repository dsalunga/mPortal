CREATE PROCEDURE [Download_Set] 
	(
		@DownloadID int = null,
		@Name nvarchar(255) = null,
		@Description nvarchar(MAX) = null,
		@FileDate datetime = null,
		@Filename nvarchar(255) = null,
		@UserId uniqueidentifier = null,
		@Rank int = null
	)
AS
	SET NOCOUNT ON
	
	if(@DownloadID is not null)
		begin
			/* UPDATE */
			UPDATE Download SET Name = @Name, Description = @Description, FileDate = @FileDate, Filename = @Filename, DateModified = GETDATE(), Rank = @Rank WHERE (DownloadID = @DownloadID)
		end
	else
		begin
			INSERT INTO Download(Name, Description, FileDate, Filename, DateModified, Rank, UserID) VALUES (@Name, @Description, @FileDate, @Filename, GETDATE(), @Rank, @UserID)
		end
	
	RETURN