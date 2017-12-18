
-- Procedure WebGlobalPolicy_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebGlobalPolicy_Get
	(
		@GlobalPolicyId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     GlobalPolicyId, Name
	FROM         WebGlobalPolicy
	WHERE
			(@GlobalPolicyId = -1 OR
				GlobalPolicyId = @GlobalPolicyId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

