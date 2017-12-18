CREATE PROCEDURE [dbo].[WebUser_Get]
	(
		@UserId int = -1,
		@UserName nvarchar(50) = NULL,
		@Email nvarchar(50) = NULL,
		@Active int = -1,
		@Status int = -2,
		@EmailId nvarchar(50) = NULL -- for alternative username
	)
AS
	SET NOCOUNT ON
	
	SELECT     UserId, UserName, [Password], FirstName, LastName, Email, Email2, MiddleName,
				LastUpdate, ActivationKey, DateCreated, NewEmail, Gender, NameSuffix,
				MobileNumber, TelephoneNumber, LastLogin, StatusText, PasswordExpiryDate,
				PhotoPath, ProviderId, [Status]
			FROM         WebUser
			WHERE (@UserId = -1 OR UserId=@UserId)
				AND (@UserName IS NULL OR UserName=@UserName)
				AND (@Email IS NULL OR Email=@Email)
				AND (@Active =-1 OR [Status]=@Active)
				AND (@Status =-2 OR [Status]=@Status)
				AND (@EmailId IS NULL OR Email LIKE @EmailId + '@%')
				

	RETURN