
-- Procedure MemberLink_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.MemberLink_Get
	(
		@MemberLinkId int = -1,
		@UserId int = -1,
		@MemberId int = -1,
		@ExternalIdNo nvarchar(50) = null,
		@MembershipDate datetime = null,
		@Approved int = -1,
		@LastUpdate datetime = NULL,
		@CelebrantsMonth int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        MemberLinkId, UserId, MemberId, ExternalIdNo, HomeAddressLine1, HomeAddressLine2, HomeAddressStateCode, HomeAddressCountryCode, HomeAddressZipCode, MobileNumber, 
	                         HomePhone, WorkAddressLine1, WorkAddressLine2, WorkAddressStateCode, WorkAddressCountryCode, WorkAddressZipCode, WorkDesignation, WorkPhone, Nickname,
	                         LastUpdate, PhotoPath, MembershipDate, Approved, Locale, Private, AdditionalInfo, LocaleId
	FROM            MemberLink
	WHERE 
			(@MemberLinkId = -1 OR MemberLinkId = @MemberLinkId) AND
			(@UserId = -1 OR UserId = @UserId) AND
			(@MemberId = -1 OR MemberId = @MemberId) AND
			(@ExternalIdNo is null OR ExternalIdNo=@ExternalIdNo) AND
			(@MembershipDate is null OR MembershipDate=@MembershipDate) AND
			(@Approved = -1 OR Approved=@Approved) AND
			(@LastUpdate IS NULL OR LastUpdate >= @LastUpdate) AND
			(@CelebrantsMonth = -1 OR MONTH(MembershipDate) = @CelebrantsMonth)
	
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

