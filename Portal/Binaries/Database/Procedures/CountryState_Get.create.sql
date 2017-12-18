
-- Procedure CountryState_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

