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