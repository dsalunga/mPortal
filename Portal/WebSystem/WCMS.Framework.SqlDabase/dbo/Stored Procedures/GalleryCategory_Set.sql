CREATE PROCEDURE [dbo].[GalleryCategory_Set]
	(
		@CategoryID int = -1,
		@Title nvarchar(256),
		@ImageURL nvarchar(256),
		@Width int = -1,
		@PhotoHeight int = 75,
		@FolderName nvarchar(250),
		@PhotoWidth int = 112,
		@DateModified datetime
	)
AS
	SET NOCOUNT ON
	
	if(@CategoryID = -1)
		begin
			/* Insert */
			INSERT INTO GalleryCategory
	                      (Title, ImageURL, Width, PhotoHeight, FolderName, PhotoWidth, DateModified)
			VALUES     (@Title, @ImageURL, @Width, @PhotoHeight, @FolderName, @PhotoWidth, @DateModified)
			
			SET @CategoryID = IDENT_CURRENT('GalleryCategory');
		end
	else
		begin
			/* Update */
			UPDATE    GalleryCategory
			SET              Title = @Title, ImageURL = @ImageURL, Width=@Width, PhotoHeight=@PhotoHeight, 
								FolderName=@FolderName, PhotoWidth=@PhotoWidth, DateModified=@DateModified
			WHERE     (CategoryID = @CategoryID)
		end
	
	SELECT @CategoryID;
	
	RETURN