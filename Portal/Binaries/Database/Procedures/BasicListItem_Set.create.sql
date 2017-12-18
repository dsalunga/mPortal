
-- Procedure BasicListItem_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [BasicListItem_Set]
	(
		@ListItemID int = null,
		@SitePageItemID int = null,
		@PageType int = null,
		
		@Field1 nvarchar(255) = null,
		@Field2 nvarchar(255) = null,
		@Field3 nvarchar(255) = null,
		@Rank int = null
	)
AS
	SET NOCOUNT ON
	
	if(@ListItemID is not null)
		begin
			UPDATE    BasicListItem
			SET              Field1 = @Field1, Field2 = @Field2, Field3 = @Field3, Rank = @Rank
			WHERE     (ListItemID = @ListItemID)
		end
	else
		begin
			INSERT INTO BasicListItem
			                      (Field1, Field2, Field3, PageType, SitePageItemID, Rank)
			VALUES     (@Field1,@Field2,@Field3,@PageType,@SitePageItemID,@Rank)
		end
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

