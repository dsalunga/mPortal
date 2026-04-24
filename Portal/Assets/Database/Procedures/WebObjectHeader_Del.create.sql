
-- Procedure WebObjectHeader_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebObjectHeader_Del]
	(
		@ObjectHeaderId int
	)
AS
	SET NOCOUNT ON
	
	IF(@ObjectHeaderId > 0)
		BEGIN
			DELETE FROM WebObjectHeader
			WHERE ObjectHeaderId = @ObjectHeaderId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

