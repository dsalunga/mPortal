
-- Procedure MenuItem_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MenuItem_Get]
	(
		@MenuID int = -2,
		@Id int = -1,
		@ParentID int = -2,
		@PageId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, NavigateURL, Text, Target, ParentId, Rank, IsActive, MenuId, PageId, Type, CheckPermission
	FROM         MenuItem
	WHERE     (@MenuID=-2 OR MenuID = @MenuID)
		AND (@Id=-1 OR Id = @Id)
		AND (@ParentId = -2 OR ParentID = @ParentID)
		AND (@PageId=-2 OR PageId=@PageId)
	ORDER BY ParentID, Rank, Text
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

