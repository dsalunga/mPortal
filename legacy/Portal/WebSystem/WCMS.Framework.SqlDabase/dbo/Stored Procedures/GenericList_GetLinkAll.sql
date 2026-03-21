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