CREATE PROCEDURE dbo.WebComment_Set
	(
		@Id int = -1,
		@Content ntext,
		@UserId int,
		@ObjectId int,
		@RecordId int,
		@DateCreated datetime,
		@ParentId int,
		@UserName nvarchar(500),
		@UserEmail nvarchar(500)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       WebComment
			SET                [Content] = @Content, UserId = @UserId, ObjectId = @ObjectId, RecordId = @RecordId, 
						DateCreated = @DateCreated, ParentId = @ParentID, UserName=@UserName, UserEmail=@UserEmail
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebComment';

			INSERT INTO WebComment
			                         ([Content], UserId, ObjectId, RecordId, DateCreated, ParentId, Id, UserName, UserEmail)
			VALUES        (@Content,@UserId,@ObjectId,@RecordId,@DateCreated,@ParentID,@Id, @UserName, @UserEmail)
		END

	SELECT @Id;

	RETURN