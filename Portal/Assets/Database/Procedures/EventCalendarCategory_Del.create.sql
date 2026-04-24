
-- Procedure EventCalendarCategory_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.EventCalendarCategory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM EventCalendarCategories
		WHERE CategoryId=@Id;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

