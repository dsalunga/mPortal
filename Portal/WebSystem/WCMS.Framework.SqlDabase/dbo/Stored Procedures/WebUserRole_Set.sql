CREATE PROCEDURE [dbo].[WebUserRole_Set]
	(
		@UserRoleId int = -1,
		@UserId int,
		@RoleId int
	)
AS
	SET NOCOUNT ON
	
	IF(@UserRoleId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebUserRole
			SET              UserId = @UserId, RoleId = @RoleId
			WHERE     (UserRoleId = @UserRoleId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @UserRoleId = WebObjects_NextId 'WebUserRole'
			
			INSERT INTO WebUserRole
			                      (UserId, RoleId, UserRoleId)
			VALUES     (@UserId,@RoleId,@UserRoleId)
		END
		
	SELECT @UserRoleId
	
	RETURN