
-- Procedure SiteListProperty_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SiteListProperty_Set]
	(
		@SitePageItemID int,
		@PageType int,
	
		@HeaderText nvarchar(256),
		@ParentID int,
		@RepeatColumns int,
		@CellPadding int,
		@SortByName bit
	)
AS
	SET NOCOUNT ON
	DECLARE @ListingPagePropertyID int
	
	SET @ListingPagePropertyID = (SELECT TOP 1 ListingPagePropertyID FROM SiteListProperty WHERE PageType=@PageType AND SitePageItemID=@SitePageItemID)
	
	if(@ListingPagePropertyID is null)
		begin
			/* INSERT */
			INSERT INTO SiteListProperty
			                      (SitePageItemID, PageType, ParentID, RepeatColumns, HeaderText, CellPadding, SortByName)
			VALUES     (@SitePageItemID,@PageType,@ParentID,@RepeatColumns,@HeaderText,@CellPadding,@SortByName)
		end
	else
		begin
			/* UPDATE */
			UPDATE    SiteListProperty
			SET              ParentID = @ParentID, RepeatColumns = @RepeatColumns, HeaderText = @HeaderText, CellPadding = @CellPadding, 
			                      SortByName = @SortByName
			WHERE     (ListingPagePropertyID = @ListingPagePropertyID)
		end
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

