CREATE PROCEDURE [StdMenuProperties_set]
	(
		@SitePageItemID int,
		@PageType int,
	
		@HeaderText nvarchar(256),
		@ListingType nvarchar(64),
		@RepeatColumns int,
		@CellPadding int
	)
AS
	SET NOCOUNT ON
	DECLARE @ListingPagePropertyID int
	
	SET @ListingPagePropertyID = (SELECT TOP 1 ListingPagePropertyID FROM StdMenuProperties WHERE PageType=@PageType AND SitePageItemID=@SitePageItemID)
	
	if(@ListingPagePropertyID is null)
		begin
			/* INSERT */
			INSERT INTO StdMenuProperties
			                      (SitePageItemID, PageType, RepeatColumns, HeaderText, ListingType, CellPadding)
			VALUES     (@SitePageItemID,@PageType,@RepeatColumns,@HeaderText,@ListingType,@CellPadding)
		end
	else
		begin
			/* UPDATE */
			UPDATE    StdMenuProperties
			SET              RepeatColumns = @RepeatColumns, HeaderText = @HeaderText, ListingType = @ListingType, CellPadding = @CellPadding
			WHERE     (ListingPagePropertyID = @ListingPagePropertyID)
		end
	
	RETURN