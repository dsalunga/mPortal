
-- Procedure WebParameterSet_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebParameterSet_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, SchemaParameterName
	FROM         WebParameterSet
	WHERE     (@Id=-1 OR Id = @Id)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

