CREATE PROCEDURE [dbo].[WebUserRole_Del]
	(
		@UserRoleId int
	)
AS
	SET NOCOUNT ON
	
	IF(@UserRoleId > 0)
		BEGIN
			DELETE FROM WebUserRole
			WHERE UserRoleId = @UserRoleId
		END
	
	RETURN