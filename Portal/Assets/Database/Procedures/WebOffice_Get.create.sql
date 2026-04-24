
-- Procedure WebOffice_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebOffice_Get
	(
		@ParentId int = -2,
		@OfficeId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        OfficeId, Name, ParentId, AddressLine1, MobileNumber, PhoneNumber, EmailAddress, ContactPerson
	FROM            WebOffice
	WHERE	(@ParentId = -2 OR ParentId=@ParentId) AND
			(@OfficeId = -1 OR OfficeId=@OfficeId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

