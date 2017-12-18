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