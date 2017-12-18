CREATE PROCEDURE [dbo].[WebUserRole_Get]
	(
		@UserRoleId int = -1,
		@UserId int = -1,
		@RoleId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     UserRoleId, UserId, RoleId
	FROM         WebUserRole
	WHERE
			(@UserRoleId = -1 OR UserRoleId=@UserRoleId)
		AND (@UserId = -1 OR UserId=@UserId)
		AND (@RoleId=-1 OR RoleId=@RoleId)
	
	RETURN