
-- Procedure IncidentTicketHistory_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentTicketHistory_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM IncidentTicketHistory
		WHERE Id=@Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

