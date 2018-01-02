
-- Procedure MCInterpreterScore_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCInterpreterScore_Set]
	@Id int = -1,
	@JudgeId int,
	@VoiceQuality int,
	@Interpretation int,
	@StagePresence int,
	@OverallImpact int,
	@DateModified datetime,
	@CandidateId int,
	@CompetitionId INT
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE	MCInterpreterScore
			SET		JudgeId=@JudgeId, VoiceQuality=@VoiceQuality, Interpretation=@Interpretation,
					StagePresence=@StagePresence, OverallImpact=@OverallImpact, DateModified=@DateModified,
					CandidateId=@CandidateId, CompetitionId=@CompetitionId
			WHERE Id=@Id;
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'MCInterpreterScore';

			INSERT INTO MCInterpreterScore
					(Id, JudgeId, VoiceQuality, Interpretation, StagePresence, OverallImpact, DateModified,
					CandidateId, CompetitionId)
			VALUES	(@Id, @JudgeId, @VoiceQuality, @Interpretation, @StagePresence, @OverallImpact, @DateModified,
					@CandidateId, @CompetitionId);
		END

	SELECT @Id;

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

