
-- Procedure BasicList_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [BasicList_Get]
	(
		@SitePageItemID int,
		@PageType int
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) BasicListID, DateCreated, UserID, PageType, SitePageItemID, RepeatColumns, ShowField2, ShowField3, CellPadding, ItemTemplate, 
	                      PageSize, GridLines, AlternatingColor, TextColor
	FROM         BasicList
	WHERE     (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

