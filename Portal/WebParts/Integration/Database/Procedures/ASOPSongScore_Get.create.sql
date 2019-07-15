
-- Procedure MCSongScore_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCSongScore_Get]
	@Id int = -1,
	@JudgeId int = -2,
	@CandidateId int = -2,
	@CompetitionId INT = -2
AS
	SET NOCOUNT ON

	SELECT Id, JudgeId, Musicality, LyricsMessage, OverallImpact, DateModified, CandidateId,
		CompetitionId
	FROM MCSongScore
	WHERE 
			(@Id=-1 OR Id=@Id)
		AND (@JudgeId = -2 OR JudgeId=@JudgeId)
		AND (@CandidateId=-2 OR CandidateId=@CandidateId)
		AND (@CompetitionId=-2 OR CompetitionId=@CompetitionId)

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

