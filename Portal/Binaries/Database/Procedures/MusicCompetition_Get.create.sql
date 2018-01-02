
-- Procedure MusicCompetition_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MusicCompetition_Get]
	@Id int = -1
AS
	SET NOCOUNT ON

	SELECT        Id, Name, ScoreLocked, CompetitionDate, Judges, VoteLocked, VoteMasked, PeoplesChoiceId,
		BestInterpreterId
	FROM            MusicCompetition
	WHERE        (@Id=-1 OR Id = @Id)
	ORDER BY Name

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

