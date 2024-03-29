
-- Procedure WebGlobalPolicy_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebGlobalPolicy_Set
	(
		@GlobalPolicyId int =-1,
		@Name nvarchar(250)
	)
AS
	SET NOCOUNT ON
	
	IF(@GlobalPolicyId > 0)
		BEGIN
			-- UPDATE
			UPDATE    WebGlobalPolicy
			SET              Name = @Name
			WHERE 
				(GlobalPolicyId = @GlobalPolicyId)
		END
	ELSE
		BEGIN
			-- INSERT
			EXEC @GlobalPolicyId = WebObject_NextId 'WebGlobalPolicy'
			
			INSERT INTO WebGlobalPolicy
			                      (GlobalPolicyId, Name)
			VALUES     (@GlobalPolicyId,@Name)
		END
	
	SELECT @GlobalPolicyId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

