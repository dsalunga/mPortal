
-- Procedure WebRole_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebRole_Set]
	(
		@RoleId int = -1,
		@Name nvarchar(250),
		@IsSystem int
	)
AS
	SET NOCOUNT ON
	
	IF(@RoleId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebRole
			SET              Name = @Name, IsSystem=@IsSystem
			WHERE     (Id = @RoleId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @RoleId = WebObjects_NextId 'WebRole'
			
			INSERT INTO WebRole
			                      (Name, Id, IsSystem)
			VALUES     (@Name,@RoleId, @IsSystem)
		END
	
	SELECT @RoleId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

