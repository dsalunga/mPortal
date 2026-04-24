
-- Procedure MenuObject_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MenuObject_Get]
	(
		@Id int = -2,
		@ObjectId int = -2,
		@RecordId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Width, Height, Horizontal, Id, MenuId, ParameterSetId, RenderMode, RecordId, ObjectId
	FROM         MenuObject
	WHERE     
			(@RecordId=-2 OR RecordId = @RecordId) 
		AND (@ObjectId=-2 OR ObjectId = @ObjectId) 
		AND (@Id=-2 OR Id = @Id)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

