CREATE PROCEDURE [StdMenu_Get]
	(
		@SitePageItemID int,
		@PageType int
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) Width, Height, Horizontal, SiteID, ShowHome, SiteSectionID, HomeText
	FROM         StdMenu
	WHERE     (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
	
	RETURN