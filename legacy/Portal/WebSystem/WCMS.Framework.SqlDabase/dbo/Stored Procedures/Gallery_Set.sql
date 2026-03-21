CREATE PROCEDURE [Gallery_Set]
	(
		@GalleryID int = -1,
		@Caption nvarchar(256),
		@Thumbnail nvarchar(256) = NULL,
		@ImageURL nvarchar(256),
		@SiteID int,
		@CategoryID int,
		@IsActive bit
	)
AS
	SET NOCOUNT ON
	
	if(@GalleryID > 0)
		begin
			-- Update
			
			UPDATE    Gallery
			SET              Caption = @Caption, Thumbnail = @Thumbnail, ImageURL = @ImageURL, SiteID = @SiteID, IsActive = @IsActive, CategoryID = @CategoryID
			WHERE     (GalleryID = @GalleryID)
		end
	else
		begin
			-- Insert
			
			INSERT INTO Gallery
	                      (Caption, Thumbnail, ImageURL, DateCreated, SiteID, IsActive, CategoryID)
			VALUES     (@Caption, @Thumbnail, @ImageURL, GETDATE(), @SiteID, @IsActive, @CategoryID)
			
			SET @GalleryID = IDENT_CURRENT('Gallery')
		end
	
	SELECT @GalleryID	
	
	RETURN