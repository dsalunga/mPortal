CREATE PROCEDURE [dbo].[ContactInquiry_CSV]
AS
	SELECT     SenderName AS [Sender Name], Subject, Message, Email, Address1, Address2, City, CountryCode AS [Country Code], StateCode AS [State Code], 
	                      ZipCode AS [Zip Code], Phone, Fax, SendTo AS [Send To], InqDateTime AS [Date & Time], SendToEmail AS [Send To], 
	                      InquiryType AS [Inquiry Type]
	FROM         Inquiries
	ORDER BY SendToEmail, InquiryType
RETURN