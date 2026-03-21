
-- Procedure MCSongScore_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCSongScore_Set]
	@Id int = -1,
	@JudgeId int,
	@Musicality int,
	@LyricsMessage int,
	@OverallImpact int,
	@DateModified datetime,
	@CandidateId int,
	@CompetitionId INT
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE	MCSongScore
			SET		JudgeId=@JudgeId, Musicality=@Musicality, LyricsMessage=@LyricsMessage,
					OverallImpact=@OverallImpact, DateModified=@DateModified, CandidateId=@CandidateId,
					CompetitionId=@CompetitionId
			WHERE	Id=@Id;
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'MCSongScore';

			INSERT INTO MCSongScore
					(Id, JudgeId, Musicality, LyricsMessage, OverallImpact, DateModified, CandidateId,
					CompetitionId)
			VALUES	(@Id, @JudgeId, @Musicality, @LyricsMessage, @OverallImpact, @DateModified, @CandidateId,
				@CompetitionId);
		END

	SELECT @Id;

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

