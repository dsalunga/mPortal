CREATE PROCEDURE [dbo].[Contact_Get]
	(
		@ContactID INT = NULL,
		@NotEmpty bit = 0
	)
AS
	SET NOCOUNT ON
	
	IF(@ContactID IS NULL)
		BEGIN
			if(@NotEmpty = 0)
				begin
					SELECT * FROM Contacts
					WHERE IsActive = 1
					ORDER BY Rank 
				end
			else
				begin
					SELECT * FROM Contacts
					WHERE IsActive = 1 AND Details <> ''
					ORDER BY Rank 
				end
		END
	ELSE
		BEGIN
			SELECT * FROM Contacts WHERE ContactID = @ContactID
		END
	RETURN