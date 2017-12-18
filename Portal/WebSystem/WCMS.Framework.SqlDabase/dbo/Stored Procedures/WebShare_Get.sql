CREATE PROCEDURE [dbo].[WebShare_Get]
	@Id int = -1,
	@ObjectId int = -2,
	@RecordId int = -2,
	@ShareObjectId int = -2,
	@ShareRecordId int = -2
AS
BEGIN
	SET NOCOUNT ON;

	SELECT      ObjectId, RecordId, ShareObjectId, ShareRecordId, Allow
	FROM        WebShare
	WHERE       (@Id = -1 OR Id=@Id)
			AND	(@ObjectId = - 2 OR
                         ObjectId = @ObjectId) 
			AND (@RecordId = - 2 OR
                         RecordId = @RecordId);

END