CREATE PROCEDURE [dbo].[WebRole_Del]
	(
		@RoleId int
	)
AS
	SET NOCOUNT ON
	
	IF(@RoleId > 0)
		BEGIN
			DELETE FROM WebRole
			WHERE Id=@RoleId
		END
	
	RETURN