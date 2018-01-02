
-- Procedure Member_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.Member_Get
	(
		@MemberId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        MemberID, ExternalIDNo, TemporaryIDNo, FirstName, MiddleName, LastName, BirthDate, BirthPlace, Gender, BloodType, CivilStatusID, CitizenshipID, RaceID, Phone, Mobile, 
	                         Email, IsActive, Flag, NickName, DateCreated, DateUpdated, MembershipDate
	FROM            Member
	WHERE (@MemberId=-1 OR MemberID=@MemberId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

