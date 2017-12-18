CREATE PROCEDURE [BasicList_Set]
	(
		@SitePageItemID int,
		@PageType int,
		
		@UserID uniqueidentifier = null,
		@RepeatColumns int = null,
		@ShowField2 bit = null,
		@ShowField3 bit = null,
		@CellPadding int = null,
		@ItemTemplate nvarchar(256) = null,
		@PageSize int = null,
		@GridLines int = null,
		@AlternatingColor nvarchar(64) = null,
		@TextColor nvarchar(64) = null
	)
AS
	SET NOCOUNT ON
	DECLARE @BasicListID int
	
	SET @BasicListID = (SELECT TOP 1 BasicListID FROM BasicList WHERE PageType=@PageType AND SitePageItemID=@SitePageItemID)
	
	if(@BasicListID is null)
		begin
			/* INSERT */
			INSERT INTO BasicList
			                      (SitePageItemID, PageType, DateCreated, UserID, RepeatColumns, ShowField2, ShowField3, CellPadding, ItemTemplate, PageSize, GridLines, 
			                      AlternatingColor, TextColor)
			VALUES     (@SitePageItemID,@PageType, 
			                      GETDATE(),@UserID,@RepeatColumns,@ShowField2,@ShowField3,@CellPadding,@ItemTemplate,@PageSize,@GridLines,@AlternatingColor,@TextColor)
		end
	else
		begin
			/* UPDATE */
			UPDATE    BasicList
			SET              CellPadding = @CellPadding, RepeatColumns = @RepeatColumns, ShowField2 = @ShowField2, ShowField3 = @ShowField3, 
			                      ItemTemplate = @ItemTemplate, PageSize = @PageSize, GridLines = @GridLines, AlternatingColor = @AlternatingColor, 
			                      TextColor = @TextColor
			WHERE     (BasicListID = @BasicListID)
		end
	
	RETURN