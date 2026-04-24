
-- Procedure StdMenu_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [StdMenu_Set]
	(
		@SitePageItemID int,
		@PageType int,
		
		@Horizontal bit = null,
		@Width nvarchar(64) = null,
		@Height nvarchar(64) = null,
		@SiteID int = null,
		@ShowHome bit = null,
		@SiteSectionID int = null,
		@HomeText nvarchar(64) = null
	)
AS
	SET NOCOUNT ON
	DECLARE @StdMenuID int
	
	SET @StdMenuID = (SELECT TOP 1 StdMenuID FROM StdMenu WHERE PageType=@PageType AND SitePageItemID=@SitePageItemID)
	
	if(@StdMenuID is null)
		begin
			/* INSERT */
			INSERT INTO StdMenu
			                      (SitePageItemID, PageType, Width, Height, Horizontal, SiteID, ShowHome, SiteSectionID, HomeText)
			VALUES     (@SitePageItemID,@PageType,@Width,@Height,@Horizontal,@SiteID,@ShowHome,@SiteSectionID,@HomeText)
		end
	else
		begin
			/* UPDATE */
			UPDATE    StdMenu
			SET              Width = @Width, Height = @Height, Horizontal = @Horizontal, SiteID = @SiteID, ShowHome = @ShowHome, SiteSectionID = @SiteSectionID, 
			                      HomeText = @HomeText
			WHERE     (StdMenuID = @StdMenuID)
		end
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

