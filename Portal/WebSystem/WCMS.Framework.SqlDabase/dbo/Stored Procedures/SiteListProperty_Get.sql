CREATE PROCEDURE [SiteListProperty_Get]
	(
		@SitePageItemID int,
		@PageType int
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) ListingPagePropertyID, PageType, SitePageItemID, ParentID, RepeatColumns, HeaderText, CellPadding, SortByName
	FROM         SiteListProperty
	WHERE     (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
	
	RETURN