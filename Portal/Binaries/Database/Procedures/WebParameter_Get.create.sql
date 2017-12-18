
-- Procedure WebParameter_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebParameter_Get
	(
		@Id int = -2,
		@ObjectId int = -2,
		@RecordId int = -2,
		@Name nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, ObjectId, RecordId, Name, Value, IsRequired
	FROM            WebParameter
	WHERE       (@ObjectId=-2 OR ObjectId = @ObjectId) 
			AND (@RecordId=-2 OR RecordId = @RecordId) 
			AND (@Name IS NULL OR Name = @Name) 
			AND (@Id = - 2 OR Id = @Id)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

