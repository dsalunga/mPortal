CREATE PROCEDURE [dbo].[GalleryCategoryLink_Get]
	@Id int = -1,
	@ObjectId int = -2,
	@RecordId int = -2
AS
	SET NOCOUNT ON

	SELECT Id, SiteId, ObjectId, RecordId, CategoryId
	FROM GalleryCategoryLink
	WHERE 
		(@Id = -1 OR Id=@Id)
	AND	(@ObjectId = -2 OR ObjectId=@ObjectId)
	AND (@RecordId = -2 OR RecordId=@RecordId)

RETURN 0