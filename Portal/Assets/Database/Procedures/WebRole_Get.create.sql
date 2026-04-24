
-- Procedure WebRole_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebRole_Get]
	(
		@RoleId int = -1,
		@Name nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, IsSystem
	FROM         WebRole
	WHERE
			(@RoleId = -1 OR Id = @RoleId)
		AND	(@Name IS NULL OR Name=@Name)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

