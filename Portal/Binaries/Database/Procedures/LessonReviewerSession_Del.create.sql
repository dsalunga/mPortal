
-- Procedure LessonReviewerSession_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.LessonReviewerSession_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id>0)
		DELETE FROM LessonReviewerSession
		WHERE Id=@Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

