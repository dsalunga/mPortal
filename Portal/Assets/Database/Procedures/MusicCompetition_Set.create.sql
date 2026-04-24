
-- Procedure MusicCompetition_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MusicCompetition_Set]
		@Id int = -1,
		@Name nvarchar(500),
		@Judges nvarchar(1000),
		@ScoreLocked int,
		@CompetitionDate datetime,
		@VoteLocked int,
		@VoteMasked int,
		@PeoplesChoiceId int,
		@BestInterpreterId int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update
			UPDATE       MusicCompetition
			SET         Name = @Name, Judges=@Judges, ScoreLocked=@ScoreLocked, CompetitionDate=@CompetitionDate,
					VoteLocked=@VoteLocked, VoteMasked=@VoteMasked, PeoplesChoiceId=@PeoplesChoiceId, BestInterpreterId=@BestInterpreterId
			WHERE        (Id = @Id);
		END
	ELSE
		BEGIN
			-- Insert
			INSERT INTO MusicCompetition
			            (Name, Judges, ScoreLocked, CompetitionDate, VoteLocked, VoteMasked, BestInterpreterId, PeoplesChoiceId)
			VALUES      (@Name, @Judges, @ScoreLocked, @CompetitionDate, @VoteLocked, @VoteMasked, @BestInterpreterId, @PeoplesChoiceId);

			SET @Id = CAST(SCOPE_IDENTITY() AS INT);
		END

	SELECT @Id;

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

