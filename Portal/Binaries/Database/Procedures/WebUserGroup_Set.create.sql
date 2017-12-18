
-- Procedure WebUserGroup_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebUserGroup_Set]
	(
		@Id int = -1,
		@UserId int,
		@GroupId int,
		@Active int,
		@DateJoined datetime,
		@ObjectId int,
		@RecordId int,
		@Remarks nvarchar(MAX),
		@CreatedById int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE    WebUserGroup
			SET     UserId = @UserId, GroupId = @GroupId, Active=@Active, DateJoined=@DateJoined, ObjectId=@ObjectId,
				RecordId=@RecordId, Remarks=@Remarks, CreatedById=@CreatedById
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @Id = WebObject_NextId 'WebUserGroup';
			
			INSERT INTO WebUserGroup
			                      (UserId, GroupId, Id, Active, DateJoined, ObjectId, RecordId, Remarks, CreatedById)
			VALUES     (@UserId,@GroupId,@Id, @Active, @DateJoined, @ObjectId, @RecordId, @Remarks, @CreatedById)
		END
		
	SELECT @Id;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

