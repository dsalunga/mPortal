
-- Procedure MemberLink_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.MemberLink_Set
	(
		@MemberLinkId int = -1,
		@UserId int,
		@MemberId int,
		@ExternalIdNo nvarchar(50),
		
		@HomeAddressLine1 nvarchar(250),
		@HomeAddressLine2 nvarchar(250),
		@HomeAddressStateCode int,
		@HomeAddressCountryCode int,
		@HomeAddressZipCode nvarchar(50),
		
		@MobileNumber nvarchar(50),
		@HomePhone nvarchar(50),
		
		@WorkAddressLine1 nvarchar(250),
		@WorkAddressLine2 nvarchar(250),
		@WorkAddressStateCode int,
		@WorkAddressCountryCode int,
		@WorkAddressZipCode nvarchar(50),
		
		@WorkDesignation nvarchar(250),
		@WorkPhone nvarchar(50),
		@Nickname nvarchar(50),
		@LastUpdate datetime,
		
		@PhotoPath nvarchar(500),
		@MembershipDate datetime,
		@Approved int,
		@Locale nvarchar(2000),
		@Private int,
		@AdditionalInfo nvarchar(4000),
		@LocaleId int
	)
AS
	SET NOCOUNT ON
	
	IF(@MemberLinkId > 0)
		BEGIN
			-- UPDATE
			
			UPDATE       MemberLink
			SET                UserId = @UserId, MemberId = @MemberId, ExternalIdNo = @ExternalIdNo, HomeAddressLine1 = @HomeAddressLine1, HomeAddressLine2 = @HomeAddressLine2,
			                         HomeAddressStateCode = @HomeAddressStateCode, HomeAddressCountryCode = @HomeAddressCountryCode, HomeAddressZipCode = @HomeAddressZipCode, 
			                         MobileNumber = @MobileNumber, HomePhone = @HomePhone, WorkAddressLine1 = @WorkAddressLine1, WorkAddressLine2 = @WorkAddressLine2, WorkAddressStateCode = @WorkAddressStateCode, 
			                         WorkAddressCountryCode = @WorkAddressCountryCode, WorkAddressZipCode = @WorkAddressZipCode, WorkDesignation = @WorkDesignation, 
			                         WorkPhone = @WorkPhone, Nickname = @Nickname, LastUpdate=@LastUpdate, PhotoPath=@PhotoPath, MembershipDate=@MembershipDate,
			                         Approved=@Approved, Locale=@Locale, Private=@Private, AdditionalInfo=@AdditionalInfo, LocaleId=@LocaleId
			WHERE        (MemberLinkId = @MemberLinkId);
		END
	ELSE
		BEGIN
			-- INSERT
			
			EXEC @MemberLinkId = WebObject_NextId 'MemberLink';
			
			INSERT INTO MemberLink
			                         (MemberLinkId, UserId, MemberId, ExternalIdNo, HomeAddressLine1, HomeAddressLine2, HomeAddressStateCode, HomeAddressCountryCode, HomeAddressZipCode, MobileNumber, 
			                         HomePhone, WorkAddressLine1, WorkAddressLine2, WorkAddressStateCode, WorkAddressCountryCode, WorkAddressZipCode, WorkDesignation, WorkPhone, Nickname,
			                         LastUpdate, PhotoPath, MembershipDate, Approved, Locale, Private, AdditionalInfo, LocaleId)
			VALUES        (@MemberLinkId,@UserId,@MemberId,@ExternalIdNo,@HomeAddressLine1,@HomeAddressLine2,@HomeAddressStateCode,@HomeAddressCountryCode,@HomeAddressZipCode,@MobileNumber,
									@HomePhone,@WorkAddressLine1,@WorkAddressLine2,@WorkAddressStateCode,@WorkAddressCountryCode,@WorkAddressZipCode,@WorkDesignation,@WorkPhone,@Nickname,
									@LastUpdate, @PhotoPath, @MembershipDate, @Approved, @Locale, @Private, @AdditionalInfo, @LocaleId);
		END
	
	SELECT @MemberLinkId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

