
-- Procedure Contact_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Contact_Set]
	(
		@ContactID INT = null,
		@Name NVARCHAR(250) = null,
		@Email NVARCHAR(250) = null,
		@Rank INT = null,
		@Details NVARCHAR(2500) = null,
		@IsActive BIT = null,
		@Subject nvarchar(256) = null
	)
AS
	SET NOCOUNT ON
	
	if(@ContactID is not null)
		begin
			UPDATE    Contacts
			SET              Name = @Name, Email = @Email, Details = @Details, Rank = @Rank, IsActive = @IsActive, Subject = @Subject
			WHERE     (ContactID = @ContactID)
		end
	else
		begin
			INSERT INTO Contacts
			                      (Name, Email, Details, Rank, IsActive, Subject)
			VALUES     (@Name,@Email,@Details,@Rank,@IsActive,@Subject)
		end
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

