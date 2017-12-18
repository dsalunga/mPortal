CREATE PROCEDURE [dbo].[Articles_Set]
	(
		@Id int = -1,
		@Title nvarchar(250),
		@Image nvarchar(250) = '',
		@Description nvarchar(1000),
		@Date DateTime,
		@Content ntext,
		@Author nvarchar(250),
		@SiteId int,
		@Active int,
		@DateModified datetime,
		@UserId int,
		@ModifiedUserId int,
		@Tags nvarchar(250)
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE    Articles
			SET              Image = @Image, Title = @Title, Description = @Description, Date = @Date, [Content] = @Content, Author = @Author, SiteId = @SiteId, 
			                      Active = @Active, DateModified = @DateModified, UserId = @UserId, ModifiedUserId = @ModifiedUserId, Tags=@Tags
			WHERE     (Id = @Id);
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @Id = WebObjects_NextId 'Articles';
			
			INSERT INTO Articles
			                      (Image, Title, Description, Date, [Content], Author, SiteId, Active, DateModified, UserId, ModifiedUserId, Id, 
								  Tags)
			VALUES     (@Image,@Title,@Description,@Date,@Content,@Author,@SiteId,@Active,@DateModified,@UserId,@ModifiedUserId,@Id, @Tags);
		END
		
	SELECT @Id;
	
	RETURN