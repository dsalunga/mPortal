
-- Procedure MCVote_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.MCVote_Set
	(
		@Id int =-1,
		@Code nvarchar(250),
		@FirstName nvarchar(250),
		@LastName nvarchar(250),
		@MobileNumber nvarchar(250),
		@Email nvarchar(250),
		@CandidateId int,
		@DateVoted datetime,
		@UserName nvarchar(250),
		@Status int,
		@CompetitionId INT,
		@IPAddress nvarchar(50),
		@Spam int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			UPDATE       MCVote
			SET                Code = @Code, FirstName = @FirstName, LastName = @LastName, MobileNumber = @MobileNumber, Email = @Email, CandidateId = @CandidateId, 
			                         DateVoted = @DateVoted, UserName=@UserName, [Status]=@Status, CompetitionId=@CompetitionId, IPAddress=@IPAddress,
									 Spam=@Spam
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			INSERT INTO MCVote
			                         (Code, FirstName, LastName, MobileNumber, Email, CandidateId, DateVoted,
									UserName, [Status], CompetitionId, IPAddress, Spam)
			VALUES        (@Code,@FirstName,@LastName,@MobileNumber,@Email,@CandidateId,@DateVoted,
							@UserName, @Status, @CompetitionId, @IPAddress, @Spam)

			SET @Id = CAST(SCOPE_IDENTITY() AS INT);
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

