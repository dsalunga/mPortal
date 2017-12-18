
-- Procedure IncidentInstance_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentInstance_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, IncidentPrefix, BaseGroup, SupportGroupPath, SLAHighDuration, SLALowDuration, SLANormalDuration, SLAWarningPercentage
	FROM         IncidentInstance
	WHERE     (@Id = -1 OR Id = @Id)
	ORDER BY [Name]

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

