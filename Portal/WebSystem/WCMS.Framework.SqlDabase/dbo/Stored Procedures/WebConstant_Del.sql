CREATE PROCEDURE [dbo].[WebConstant_Del]
	(
		@ConstantId int
	)
AS
	SET NOCOUNT ON
	
	IF(@ConstantId > 0)
		BEGIN
			DELETE FROM WebConstant
			WHERE ConstantId = @ConstantId
		END
	
	RETURN