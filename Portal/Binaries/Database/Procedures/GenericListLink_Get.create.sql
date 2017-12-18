
-- Procedure GenericListLink_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericListLink_Get]
	(
		@RecordId int,
		@ObjectId int
	)
AS
	SET NOCOUNT ON
	SELECT     p.Id, p.Title, p.IsActive, p.Description
	FROM         GenericListLink sp INNER JOIN
	                      GenericList p ON sp.ListId = p.Id
	WHERE     (sp.RecordId = @RecordId) AND (sp.ObjectId = @ObjectId)
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

