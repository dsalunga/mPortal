CREATE PROCEDURE [BasicListItem_Get]
	(
		@SitePageItemID int = null,
		@PageType int = null,
		
		@ListItemID int = null,
		@PageSize int = null,
		@Page int = null
	)
AS
	SET NOCOUNT ON
	
	if(@ListItemID is not null)
		begin
			SELECT     ListItemID, Field1, Field2, Field3, PageType, SitePageItemID, Rank
			FROM         BasicListItem
			WHERE     (ListItemID = @ListItemID)
		end
	else if(@Page is not null and @PageSize is not null)
		begin
			SELECT TOP (@PageSize) ListItemID, Field1, Field2, Field3, PageType, SitePageItemID, Rank 
			FROM BasicListItem
			WHERE      (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
			AND ListItemID NOT IN
			(
				SELECT TOP ((@Page-1)*@PageSize) ListItemID 
				FROM BasicListItem
				WHERE (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
				ORDER BY Rank, Field1, Field2, Field3
			)
			ORDER BY Rank, Field1, Field2, Field3
		end
	else
		begin
			SELECT     ListItemID, Field1, Field2, Field3, PageType, SitePageItemID, Rank
			FROM         BasicListItem
			WHERE      (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
			ORDER BY Rank, Field1, Field2, Field3
		end
	
	RETURN