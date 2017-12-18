CREATE PROCEDURE [dbo].[Newsletter_Get]
	@Id int = -1,
	@SiteId int = -2,
	@ObjectId int = -2,
	@RecordId int = -2,
	@Email nvarchar(50) = NULL
AS
	SELECT Id, Name, Email, IPAddress, SubscribeDate, ObjectId, RecordId, SiteId, Gender
	FROM Newsletter
	WHERE
		(@Id = -1 OR Id=@Id)
		AND (@SiteId= -2 OR SiteId=@SiteId)
		AND (@ObjectId = -2 OR ObjectId=@ObjectId)
		AND (@RecordId = -2 OR RecordId=@RecordId)
		AND (@Email IS NULL OR Email=@Email)

	ORDER BY SubscribeDate

RETURN 0