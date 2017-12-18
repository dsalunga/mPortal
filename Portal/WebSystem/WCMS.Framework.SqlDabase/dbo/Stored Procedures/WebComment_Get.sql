CREATE PROCEDURE dbo.WebComment_Get
	(
		@Id int = -2,
		@UserId int = -2,
		@ObjectId int = -2,
		@RecordId int = -2,
		@ParentId int = -2
	)
AS
	SET NOCOUNT ON

	SELECT      Id, [Content], UserId, ObjectId, RecordId, DateCreated, ParentId,
				UserName, UserEmail
	FROM        WebComment
	WHERE   (@Id = -2 OR Id = @Id) 
		AND (@UserId = -2 OR UserId = @UserId) 
		AND (@ObjectId = -2 OR ObjectId = @ObjectId) 
		AND (@RecordId = -2 OR RecordId = @RecordId) 
		AND (@ParentId = -2 OR ParentId = @ParentId)

	RETURN