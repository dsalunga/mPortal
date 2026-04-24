
-- Procedure GenericList_GetLinkAll
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericList_GetLinkAll]
	(
		@RecordId int,
		@ObjectId int
	)
AS
	SET NOCOUNT ON
	
			/* SELECT ALL (NO FILTER) */
			SELECT     Id, Title, IsActive
			FROM         GenericList p
			WHERE     (Id NOT IN
			                          (SELECT     ListId
			                            FROM          GenericListLink
			                            WHERE      RecordId = @RecordId AND ObjectId = @ObjectId))

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

