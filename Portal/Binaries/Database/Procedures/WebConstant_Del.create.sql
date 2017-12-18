
-- Procedure WebConstant_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

