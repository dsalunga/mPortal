
-- Procedure WebObjectContent_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebObjectContent_Del]
	(
		@ObjectContentId int
	)
AS
	SET NOCOUNT ON
	
	if(@ObjectContentId > 0)
		BEGIN
			DELETE FROM WebObjectContent
			WHERE ObjectContentId=@ObjectContentId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

