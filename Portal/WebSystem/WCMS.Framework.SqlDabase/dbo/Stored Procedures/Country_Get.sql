CREATE PROCEDURE [dbo].[Country_Get]
(
	@CountryCode int = null,
	@CountryName nvarchar(100) = NULL,
	@ISOCode nvarchar(50) = NULL
)
AS
	SET NOCOUNT ON
	
	SELECT     CountryCode, CountryName, Description, ISOCode, DialingCode, MaxPhoneDigit, RegionCode
	FROM         Country
	WHERE (@CountryCode IS NULL OR CountryCode = @CountryCode)
		AND (@CountryName IS NULL OR CountryName=@CountryName)
		AND (@ISOCode IS NULL OR ISOCode=@ISOCode)
	ORDER BY CountryName
	
	
	RETURN