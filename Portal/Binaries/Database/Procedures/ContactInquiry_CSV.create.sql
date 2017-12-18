
-- Procedure ContactInquiry_CSV
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactInquiry_CSV]
AS
	SELECT     SenderName AS [Sender Name], Subject, Message, Email, Address1, Address2, City, CountryCode AS [Country Code], StateCode AS [State Code], 
	                      ZipCode AS [Zip Code], Phone, Fax, SendTo AS [Send To], InqDateTime AS [Date & Time], SendToEmail AS [Send To], 
	                      InquiryType AS [Inquiry Type]
	FROM         Inquiries
	ORDER BY SendToEmail, InquiryType
RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

