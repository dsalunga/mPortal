CREATE PROCEDURE [StdMenuProperties_Get]
	(
		@SitePageItemID int,
		@PageType int
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) ListingPagePropertyID, PageType, SitePageItemID, ParentID, RepeatColumns, HeaderText, ListingType, CellPadding
	FROM         StdMenuProperties
	WHERE     (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
	
	RETURN