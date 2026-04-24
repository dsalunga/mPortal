
-- Procedure WebOffice_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebOffice_Set
	(
		@OfficeId int = -1,
		@Name nvarchar(250),
		@ParentId int,
		@AddressLine1 nvarchar(250),
		@MobileNumber nvarchar(50),
		@PhoneNumber nvarchar(50),
		@EmailAddress nvarchar(50),
		@ContactPerson nvarchar(250)
	)
AS
	SET NOCOUNT ON
	
	IF(@OfficeId > 0)
		BEGIN
			-- Update
			
			UPDATE       WebOffice
			SET                OfficeId = @OfficeId, Name = @Name, ParentId = @ParentId, AddressLine1 = @AddressLine1, MobileNumber = @MobileNumber, PhoneNumber = @PhoneNumber, 
			                         EmailAddress = @EmailAddress, ContactPerson = @ContactPerson
			WHERE	(OfficeId=@OfficeId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @OfficeId = WebObject_NextId 'WebOffice'
			
			INSERT INTO WebOffice
			                         (OfficeId, Name, ParentId, AddressLine1, MobileNumber, PhoneNumber, EmailAddress, ContactPerson)
			VALUES        (@OfficeId,@Name,@ParentId,@AddressLine1,@MobileNumber,@PhoneNumber,@EmailAddress,@ContactPerson)
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

