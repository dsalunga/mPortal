CREATE PROCEDURE [dbo].[CountryState_Get]
(
	@StateCode int = null,
	@CountryCode int = -1
)
AS
	SET NOCOUNT ON
	
	if(@StateCode is not null)
		begin
			SELECT     StateCode, StateName, CountryCode, ZipCode
			FROM         CountryState
			WHERE 
					(@StateCode IS NULL OR StateCode = @StateCode)
				AND	(@CountryCode=-1 OR CountryCode=@CountryCode)
		end
	
	
	RETURN