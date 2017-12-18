-- =============================================
-- Author:		AF
-- Create date: 10 June 2009
-- Description:	To insert new member information
-- =============================================
CREATE PROCEDURE [dbo].[MemberInfo_Set]
(@MemberID bigint
,@IDNo varchar(10)
,@LocaleIDNo varchar(10)
,@FirstName varchar(30)
,@MiddleName varchar(30)
,@LastName varchar(30)
,@BirthDate smalldatetime
,@BirthPlace varchar(200)
,@Gender char(1)
,@BloodType varchar(3)
,@CivilStatusID smallint
,@CitizenshipID smallint
,@RaceID smallint
,@Phone varchar(20)
,@Mobile varchar(20)
,@Email varchar(100)
,@IsActive bit)

AS
BEGIN
SET NOCOUNT ON;

	IF NOT EXISTS(SELECT MemberID FROM Members WHERE IDNo=@IDNo)
	BEGIN
		INSERT INTO Members
		VALUES (@MemberID, @IDNo,@LocaleIDNo,@FirstName,@MiddleName,@LastName,
			@BirthDate, @BirthPlace, @Gender, @BloodType,@CivilStatusID,
			@CitizenshipID, @RaceID, @Phone, @Mobile, @Email, @IsActive)
	END
END