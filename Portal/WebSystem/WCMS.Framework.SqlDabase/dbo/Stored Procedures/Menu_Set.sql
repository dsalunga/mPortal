CREATE PROCEDURE Menu_Set
	(
		@Id int = -1,
		@Name nvarchar(256),
		@SiteId int = -1,
		@UserId int = -1,
		@PageId int = -1,
		@IsActive int,
		@IncludeChildren int = 0
	)
AS
	SET NOCOUNT ON
	
	if(@Id = -1)
		begin
			/* INSERT */
			
			EXEC @Id = WebObject_NextId 'Menu';
			
			INSERT INTO Menu
			                      (Id,Name, IsActive, DateCreated, UserId, SiteId, PageId, IncludeChildren)
			VALUES     (@Id, @Name,@IsActive, GETDATE(),@UserId,@SiteId, @PageId, @IncludeChildren)
		end
	else
		begin
			/* UPDATE */
			
			UPDATE    Menu
			SET              Name = @Name, IsActive = @IsActive, SiteId = @SiteId,
							PageId=@PageId, IncludeChildren=@IncludeChildren
			WHERE     (Id = @Id)
		end

	SELECT @Id

	RETURN