
-- Procedure WebConstant_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebConstant_Get]
	(
		@ConstantId int = -1,
		@ObjectId int = -2,
		@Category nvarchar(50) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT     ConstantId, Value, Rank, Category, [Text], ObjectId
	FROM         WebConstant
	WHERE     (@ConstantId < 1 OR ConstantId = @ConstantId)
	    AND (@Category IS NULL OR Category = @Category)
		AND (@ObjectId=-2 OR ObjectId=@ObjectId)
	ORDER BY Rank
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

