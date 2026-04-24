
-- Procedure IncidentTicket_GetMaxCount
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentTicket_GetMaxCount
	(
		@Date datetime
	)
AS
	SET NOCOUNT ON

	SELECT COUNT(1) FROM IncidentTicket
	WHERE
			DAY(DateCreated) = DAY(@Date) 
		AND MONTH(DateCreated) = MONTH(@Date) 
		AND YEAR(DateCreated) = YEAR(@Date)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

