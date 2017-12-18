
-- Procedure WebPartAdmin_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartAdmin_Del]
	(
		@PartAdminId int
	)
AS
	SET NOCOUNT ON
	
	if(@PartAdminId > 0)
		BEGIN
			DELETE FROM WebPartAdmin
			WHERE PartAdminId=@PartAdminId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

