
-- Procedure WebUser_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebUser_Del]
	(
		@UserId int = -1,
		@UserName nvarchar(50) = null
	)
AS
	SET NOCOUNT ON
	
	IF(@UserId > 0)
		BEGIN
			DELETE FROM WebUser 
				WHERE UserId=@UserId
		END
	ELSE IF(@UserName <> NULL)
		BEGIN
			DELETE FROM WebUser 
				WHERE UserName=@UserName
		END
		
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

