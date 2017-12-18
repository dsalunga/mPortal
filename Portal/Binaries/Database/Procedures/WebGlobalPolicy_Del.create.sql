
-- Procedure WebGlobalPolicy_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebGlobalPolicy_Del]
	(
		@GlobalPolicyId int
	)
AS
	SET NOCOUNT ON
	
	IF(@GlobalPolicyId > 0)
		BEGIN
			DELETE FROM WebGlobalPolicy
			WHERE GlobalPolicyId=@GlobalPolicyId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

