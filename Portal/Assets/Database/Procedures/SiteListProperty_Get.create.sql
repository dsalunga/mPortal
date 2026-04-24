
-- Procedure SiteListProperty_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

