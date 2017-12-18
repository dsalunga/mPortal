CREATE PROCEDURE dbo.WebFolder_Set
	(
		@Id int =-1,
		@Name nvarchar(250),
		@ParentId int,
		@ShareName nvarchar(250),
		@ObjectId int,
		@SiteId int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE       WebFolder
			SET                Id = @Id, Name = @Name, ParentId = @ParentId, ShareName = @ShareName,
								ObjectId =@ObjectId, SiteId=@SiteId
			WHERE Id=@Id
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'WebFolder'
			
			INSERT INTO WebFolder
			                         (Id, Name, ParentId, ShareName, ObjectId, SiteId)
			VALUES        (@Id,@Name,@ParentId,@ShareName, @ObjectId, @SiteId)
		END
		
	SELECT @Id
	
	RETURN