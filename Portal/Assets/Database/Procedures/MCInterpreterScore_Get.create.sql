
-- Procedure MCInterpreterScore_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCInterpreterScore_Get]
	@Id int = -1,
	@CandidateId int = -2,
	@JudgeId int = -2,
	@CompetitionId int = -2
AS
	SET NOCOUNT ON

	SELECT	Id, JudgeId, VoiceQuality, Interpretation, StagePresence, OverallImpact,
			DateModified, CandidateId, CompetitionId
	FROM	MCInterpreterScore
	WHERE	
			(@Id = -1 OR Id=@Id)
		AND	(@CandidateId=-2 OR CandidateId=@CandidateId)
		AND (@JudgeId=-2 OR JudgeId=@JudgeId)
		AND (@CompetitionId=-2 OR CompetitionId=@CompetitionId);

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

