CREATE PROCEDURE [dbo].[Contact_CMS]
	(
		@ContactID INT = NULL
	)
AS
	SET NOCOUNT ON
	
	IF(@ContactID IS NULL)
		BEGIN
			SELECT * FROM Contacts ORDER BY Rank DESC
		END
	ELSE
		BEGIN
			SELECT * FROM Contacts WHERE ContactID = @ContactID
		END
	RETURN