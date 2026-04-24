
-- Procedure MCCandidate_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCCandidate_Set]
	(
		@Id int = -1,
		@Name nvarchar(250),
		@Entry nvarchar(2000),
		@Lyrics nvarchar(MAX),
		@SourceUrl nvarchar(500),
		@SourceUrl2 nvarchar(500),
		@Lyricist nvarchar(250),
		@Interpreter nvarchar(250),
		@PhotoFile nvarchar(500),
		@CompetitionId int,
		@Rank int,
		@WinnerRank int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       MCCandidate
			SET         Name = @Name, Entry = @Entry, Lyrics = @Lyrics, SourceUrl = @SourceUrl, 
						SourceUrl2 = @SourceUrl2, Lyricist=@Lyricist, Interpreter = @Interpreter,
						PhotoFile=@PhotoFile, CompetitionId=@CompetitionId, Rank=@Rank,
						WinnerRank=@WinnerRank
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			INSERT INTO MCCandidate
			    (Name, Entry, Lyrics, SourceUrl, SourceUrl2, Lyricist, Interpreter, PhotoFile,
					CompetitionId, Rank, WinnerRank)
			VALUES  (@Name,@Entry,@Lyrics,@SourceUrl,@SourceUrl2, @Lyricist, @Interpreter, @PhotoFile,
					@CompetitionId, @Rank, @WinnerRank)

			SET @Id = CAST(SCOPE_IDENTITY() AS INT);
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

