
-- Procedure ContactInquiry_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactInquiry_Get]
	(
		@InquiryID INT = -1,
		@RecordId int = -1,
		@ObjectId int = -1
	)
AS
	SET NOCOUNT ON
	
	IF(@InquiryID = -1)
		BEGIN
			SELECT     InquiryID, SenderName, Subject, Message, Email, Address1, Address2, City, 
						CountryCode, StateCode, ZipCode, Phone, Fax, SendTo, InqDateTime, 
					    IsActive, SendToEmail, InquiryType, RecordId, ObjectId, UserId
			FROM         ContactInquiry
			WHERE     (@RecordId = -1 OR RecordId = @RecordId) 
					AND (@ObjectId =-1 OR ObjectId=@ObjectId)
			ORDER BY InqDateTime DESC
		END
	ELSE
		BEGIN
			SELECT I.InquiryID, I.SenderName, I.Subject, I.Message, I.Email, I.Address1, I.Address2, 
					I.City, I.CountryCode, I.StateCode, I.ZipCode, I.Phone, I.Fax, I.SendTo, I.UserId,
					I.InqDateTime, I.IsActive, I.SendToEmail, I.InquiryType, I.RecordId, S.StateName, 
					C.CountryName 
			FROM ContactInquiry AS I 
				LEFT OUTER JOIN USState AS S 
					ON I.StateCode = S.StateCode 
				LEFT OUTER JOIN Country AS C 
					ON I.CountryCode = C.CountryCode 
			WHERE (I.InquiryID = @InquiryID)
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

