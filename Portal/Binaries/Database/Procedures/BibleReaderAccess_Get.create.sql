
-- Procedure BibleReaderAccess_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.BibleReaderAccess_Get
	(
		@Id int = -1,
		@UserId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, UserId, AppAccessCount, LastAccessed
	FROM            BibleReaderAccess
	WHERE        (@Id = -1 OR Id = @Id)
		AND (@UserId = -1 OR UserId=@UserId)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

