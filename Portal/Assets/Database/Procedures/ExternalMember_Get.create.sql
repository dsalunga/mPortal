
-- Procedure ExternalMember_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ExternalMember_Get]
	(
		@MemberId int = -1,
		@Keyword nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT        MemberID, ExternalIDNo, TemporaryIDNo, FirstName, MiddleName, LastName, BirthDate, BirthPlace, Gender, BloodType, CivilStatusID, CitizenshipID, RaceID, Phone, Mobile, 
	                         Email, IsActive, Flag, NickName, DateCreated, DateUpdated
	FROM            [ExternalDB].dbo.Members
	WHERE (@MemberId=-1 OR MemberID=@MemberId) AND
		(@Keyword IS NULL OR 
			(ExternalIDNo LIKE '%' + @Keyword + '%') OR
			(TemporaryIDNo LIKE '%' + @Keyword + '%') OR
			(FirstName LIKE '%' + @Keyword + '%') OR
			(LastName LIKE '%' + @Keyword + '%') OR
			(MiddleName LIKE '%' + @Keyword + '%') OR
			(Email LIKE '%' + @Keyword + '%')
		)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

