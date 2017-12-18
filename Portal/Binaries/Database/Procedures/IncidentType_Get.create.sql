
-- Procedure IncidentType_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentType_Get
	(
		@Id int = -1,
		@InstanceId int =-2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, FollowStdSLA, Rank
	FROM         IncidentType
	WHERE     (@Id = -1 OR Id = @Id)
		AND (@InstanceId=-2 OR InstanceId=@InstanceId)
	ORDER BY Rank, [Name]

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

