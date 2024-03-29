﻿CREATE PROCEDURE [dbo].[WebUserGroup_Get]
	(
		@Id int = -1,
		@UserId int = -1,
		@GroupId int = -1,
		@Active int = -1,
		@ObjectId int =-2,
		@RecordId int =-2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, UserId, GroupId, Active, DateJoined, RecordId, ObjectId, Remarks
	FROM         WebUserGroup
	WHERE
			(@Id = -1 OR Id=@Id)
		AND (@UserId = -1 OR UserId=@UserId)
		AND (@GroupId=-1 OR GroupId=@GroupId)
		AND (@Active = -1 OR Active=@Active)
		AND (@ObjectId=-2 OR ObjectId=@ObjectId)
		AND (@RecordId=-2 OR RecordId=@RecordId)
	
	RETURN