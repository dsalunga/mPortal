CREATE PROCEDURE [dbo].[WebPermissionSet_Get]
	(
		@Id int = -1,
		@ObjectId int= -1,
		@Public int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, ObjectId, PermissionId, [Public]
	FROM         WebPermissionSet
	WHERE
		(@Id =-1 OR Id=@Id) AND
		(@ObjectId =-1 OR ObjectId=@ObjectId) AND
		(@Public = -1 OR [Public]=@Public)
	
	RETURN