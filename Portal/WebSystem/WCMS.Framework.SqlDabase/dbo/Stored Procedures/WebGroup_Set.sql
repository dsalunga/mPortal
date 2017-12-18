CREATE PROCEDURE [dbo].[WebGroup_Set]
	(
		@Id int = -1,
		@Name nvarchar(250),
		@IsSystem int,
		@OwnerId int,
		@ParentId int,
		@JoinApproval int,
		@JoinAlert int,
		@PageUrl nvarchar(250),
		@PageId int,
		@Description nvarchar(MAX),
		@Managers nvarchar(MAX)
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE    WebGroup
			SET              Name = @Name, IsSystem=@IsSystem, OwnerId=@OwnerId, ParentId =@ParentId,
							JoinApproval=@JoinApproval, JoinAlert=@JoinAlert, PageUrl=@PageUrl,
							PageId=@PageId, Description=@Description, Managers=@Managers
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'WebGroup';
			
			INSERT INTO WebGroup
			                      (Name, Id, IsSystem, OwnerId, ParentId, JoinApproval, JoinAlert, PageUrl,
								  PageId, Description, Managers)
			VALUES		(@Name,@Id, @IsSystem, @OwnerId, @ParentId, @JoinApproval, @JoinAlert, @PageUrl,
							@PageId, @Description, @Managers)
		END
	
	SELECT @Id
	
	RETURN