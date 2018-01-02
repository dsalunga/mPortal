
-- Procedure ODKVisit_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.ODKVisit_Get
	(
		@Id int = -1,
		@GroupId int = -1,
		@UserId int = -2,
		@Tag nvarchar(100) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, CreatedUserId, DateCreated, ActualReport, Status, GroupId, [Name], VisitedUserId, DateVisited,
				ActionTaken, ContactNo, TimesVisited, Address, MembershipDate, Tags
	FROM            ODKVisit
	WHERE
		(@Id = -1 OR Id=@Id)
		AND (@GroupId = -1 OR GroupId=@GroupId)
		AND (@UserId = -2 OR VisitedUserId=@UserId)
		AND (@Tag IS NULL OR ','+Tags+',' LIKE '%,'+@Tag+',%')
	ORDER BY DateVisited DESC

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

