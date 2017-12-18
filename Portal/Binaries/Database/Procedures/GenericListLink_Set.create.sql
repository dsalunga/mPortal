
-- Procedure GenericListLink_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [GenericListLink_Set]
	(
		@ListId int,
		@RecordId int,
		@ObjectId int,
		@SiteId int
	)
AS
	SET NOCOUNT ON
	

	INSERT INTO GenericListLink
	                      (ListId, ObjectId, RecordId, SiteID)
	VALUES     (@ListId, @ObjectId, @RecordId, @SiteId)
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

