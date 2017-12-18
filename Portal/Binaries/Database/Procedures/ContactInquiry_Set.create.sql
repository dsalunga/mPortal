
-- Procedure ContactInquiry_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactInquiry_Set]
	(
		@SenderName NVARCHAR(256),
		@Subject NVARCHAR(256),
		@Message TEXT,
		@InquiryType NVARCHAR(256),
		@Email NVARCHAR(256),
		
		@Address1 NVARCHAR(256),
		@Address2 NVARCHAR(256),
		@City NVARCHAR(256),
		@CountryCode int,
		@StateCode int,
		@ZipCode NVARCHAR(63),
		
		@Phone NVARCHAR(256),
		@Fax NVARCHAR(256),
		@SendTo NVARCHAR(256),
		@SendToEmail NVARCHAR(256),
		
		@RecordId int,
		@ObjectId int,
		@UserId int
	)
AS
	SET NOCOUNT ON
	INSERT INTO ContactInquiry(SenderName, Subject, Message, Email, Phone, Fax, SendTo, InqDateTime, 
							IsActive, SendToEmail, InquiryType, Address1, Address2, City, CountryCode, 
							StateCode, ZipCode, ObjectId, RecordId, UserId) 
		VALUES (@SenderName, @Subject, @Message, @Email, @Phone, @Fax, @SendTo, GETDATE(), 0, 
				@SendToEmail, @InquiryType, @Address1, @Address2, @City, @CountryCode, @StateCode, 
				@ZipCode, @ObjectId, @RecordId, @UserId)
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

