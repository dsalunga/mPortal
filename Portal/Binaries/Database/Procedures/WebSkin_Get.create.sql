
-- Procedure WebSkin_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebSkin_Get
	(
		@Id int = -1,
		@ObjectId int = -2,
		@RecordId int = -2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, ObjectId, RecordId, Rank
	FROM         WebSkin
	WHERE     (@Id = -1 OR Id = @Id)
		AND (@ObjectId=-2 OR ObjectId=@ObjectId)
		AND (@RecordId=-2 OR RecordId=@RecordId)
	ORDER BY [Name]

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

