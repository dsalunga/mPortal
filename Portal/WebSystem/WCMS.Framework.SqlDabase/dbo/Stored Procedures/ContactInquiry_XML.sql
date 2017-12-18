CREATE PROCEDURE [dbo].[ContactInquiry_XML]
	(
		@SiteID int = null
	)
AS
	SET NOCOUNT ON
	
			if(@SiteID is null)
				begin
					SELECT     I.InquiryID AS ID, I.SenderName AS Name, I.Subject, I.Message, I.Email AS [E-mail Address], I.Address1, I.Address2, I.City, 
					                      I.CountryCode AS [Country Code], I.StateCode AS [State Code], I.ZipCode AS Zip, I.Phone, I.Fax, I.SendTo AS [Sent To], 
					                      I.SendToEmail AS [Sent To Email], I.InqDateTime AS [Date/Time], I.IsActive AS [Is Read], I.InquiryType AS [Inquiry Type], 
					                      S.SiteName AS [Web Site]
					FROM         Inquiries AS I LEFT OUTER JOIN
					                      CMS.WebSites AS S ON I.SiteID = S.SiteID
					ORDER BY [Date/Time] DESC
				end
			else
				begin
					SELECT     I.InquiryID AS ID, I.SenderName AS Name, I.Subject, I.Message, I.Email AS [E-mail Address], I.Address1, I.Address2, I.City, 
					                      I.CountryCode AS [Country Code], I.StateCode AS [State Code], I.ZipCode AS Zip, I.Phone, I.Fax, I.SendTo AS [Sent To], I.InqDateTime AS [Date/Time], 
					                      I.IsActive AS [Is Read], I.SendToEmail AS [Sent To Email], I.InquiryType AS [Inquiry Type], S.SiteName AS [Web Site]
					FROM         Inquiries AS I INNER JOIN
					                      CMS.WebSites AS S ON I.SiteID = S.SiteID
					WHERE     (I.SiteID = @SiteID)
					ORDER BY [Date/Time] DESC
				end
	
	RETURN