
-- Procedure MCCandidate_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCCandidate_Get]
	(
		@Id int = -1,
		@CompetitionId INT = -2
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name, Entry, Lyrics, SourceUrl, SourceUrl2, Lyricist, Interpreter, PhotoFile,
		CompetitionId, Rank, WinnerRank
	FROM            MCCandidate
	WHERE        (@Id=-1 OR Id = @Id)
		AND (@CompetitionId=-2 OR CompetitionId=@CompetitionId)
	ORDER BY Name

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

