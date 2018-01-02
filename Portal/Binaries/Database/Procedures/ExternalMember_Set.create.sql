
-- Procedure ExternalMember_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ExternalMember_Set]
	(
		@MemberId int =-1,
		@ExternalIDNo nvarchar(250),
		@TemporaryIDNo nvarchar(250),
		@FirstName nvarchar(250),
		@MiddleName nvarchar(250),
		@LastName nvarchar(250),
		@Email nvarchar(250)

		/*
		@BirthDate datetime,
		@BirthPlace nvarchar(250),
		@Gender char(1),
		@BloodType nvarchar(5),
		@CivilStatusId int,
		@CitizenshipId int,
		@RaceId int,
		@Phone nvarchar(250),
		@Mobile nvarchar(250),
		
		@IsActive int,
		@Flag char(1),
		@NickName nvarchar(250),
		@DateCreated datetime,
		@DateUpdated datetime,
		@MembershipDate datetime
		*/
	)
AS
	SET NOCOUNT ON
	
	IF(@MemberId > 0)
		BEGIN
			-- Update
			UPDATE       [ExternalDB].dbo.Members
			SET                ExternalIDNo = @ExternalIDNo, TemporaryIDNo = @TemporaryIDNo, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, 
									Email = @Email 
									/*
			                         BirthDate = @BirthDate, BirthPlace = @BirthPlace, Gender = @Gender, BloodType = @BloodType, CivilStatusID = @CivilStatusId, 
			                         CitizenshipID = @CitizenshipId, RaceID = @RaceId, Phone = @Phone, Mobile = @Mobile, IsActive = @IsActive, 
			                         Flag=@Flag, NickName=@NickName, DateCreated=@DateCreated, DateUpdated=@DateUpdated, MembershipDate=@MembershipDate 
									 */
			WHERE        (MemberID = @MemberId)
		END
	
	/*ELSE
		BEGIN
			-- Insert
			-- negative value
			SET @MemberId = @MemberId * -1;
			
			INSERT INTO ExternalDB.dbo.Members
			                         (ExternalIDNo, TemporaryIDNo, FirstName, MiddleName, LastName, BirthDate, BirthPlace, Gender, BloodType, CivilStatusID, CitizenshipID, 
			                         RaceID, Phone, Mobile, Email, IsActive, MemberID, Flag, NickName, DateCreated, DateUpdated, MembershipDate)
			VALUES        (@ExternalIDNo,@TemporaryIDNo,@FirstName,@MiddleName,@LastName,@BirthDate,@BirthPlace,@Gender,@BloodType,@CivilStatusId,@CitizenshipId,
							@RaceId,@Phone,@Mobile,@Email,@IsActive,@MemberId, @Flag, @NickName, @DateCreated, @DateUpdated, @MembershipDate)
		END
	*/

	SELECT @MemberId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

