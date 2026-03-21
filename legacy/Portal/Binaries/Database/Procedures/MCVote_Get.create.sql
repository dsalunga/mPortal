
-- Procedure MCVote_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.MCVote_Get
	(
		@Id int =-1,
		@CandidateId int = -2,
		@Code nvarchar(250) = NULL,
		@UserName nvarchar(250) = NULL,
		@Email nvarchar(250) = NULL,
		@Status int = -1,
		@CompetitionId INT = -2
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Code, FirstName, LastName, MobileNumber, Email, 
		CandidateId, DateVoted, UserName, Status, CompetitionId, IPAddress,
		Spam
	FROM            MCVote
	WHERE        (@Id=-1 OR Id = @Id) 
			AND (@Code IS NULL OR Code = @Code)
			AND (@UserName IS NULL OR UserName=@UserName)
			AND (@CandidateId =-2 OR CandidateId = @CandidateId)
			AND (@Email IS NULL OR 
					(CHARINDEX('@gmail.com', Email) <= 0 AND CHARINDEX('@googlemail.com', Email) <= 0 AND Email=@Email) OR 
					((CHARINDEX('@gmail.com', Email) > 0 OR CHARINDEX('@googlemail.com', Email) > 0) AND REPLACE(Email, '.', '') = REPLACE(@Email, '.', ''))
				)
			AND (@Status = -1 OR [Status]=@Status)
			AND (@CompetitionId=-2 OR CompetitionId=@CompetitionId)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

