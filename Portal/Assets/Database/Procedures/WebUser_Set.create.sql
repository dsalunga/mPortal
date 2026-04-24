
-- Procedure WebUser_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebUser_Set]
	(
		@UserId int = -1,
		@Password nvarchar(1000),
		@UserName nvarchar(50),
		@FirstName nvarchar(50),
		@LastName nvarchar(50),
		@MiddleName nvarchar(250),
		@Email nvarchar(250),
		@Email2 nvarchar(250),
		@ActivationKey nvarchar(50),
		@LastUpdate datetime,
		@DateCreated datetime,
		@NewEmail nvarchar(250),
		@Gender nchar(1),
		@NameSuffix nvarchar(50),
		@MobileNumber nvarchar(50),
		@TelephoneNumber nvarchar(50),
		@LastLogin datetime,
		@StatusText nvarchar(1500),
		@PasswordExpiryDate datetime,
		@PhotoPath nvarchar(500),
		@ProviderId int,
		@Status int,
		@MaritalStatusId int,
		@LastLoginFailureDate datetime,
		@LoginFailureCount int
	)
AS
	SET NOCOUNT ON
	
	if(@UserId > 0)
		begin
			-- Update
			
			UPDATE    WebUser
			SET              UserName = @UserName, Password = @Password, FirstName = @FirstName, LastName = @LastName, Email=@Email,
							MiddleName = @MiddleName, LastUpdate = @LastUpdate, ActivationKey = @ActivationKey,
							DateCreated=@DateCreated, NewEmail=@NewEmail, Email2=@Email2, Gender=@Gender, NameSuffix=@NameSuffix,
							MobileNumber=@MobileNumber, TelephoneNumber=@TelephoneNumber, LastLogin=@LastLogin, StatusText=@StatusText,
							PasswordExpiryDate=@PasswordExpiryDate, PhotoPath=@PhotoPath, ProviderId=@ProviderId, [Status]=@Status,
							MaritalStatusId=@MaritalStatusId, LastLoginFailureDate=@LastLoginFailureDate, LoginFailureCount=@LoginFailureCount
			WHERE     (UserId = @UserId)
		end
	else
		begin
			-- Insert
			EXEC @UserId = WebObject_NextId 'WebUser';
			
			INSERT INTO WebUser
			                         (UserName, Password, FirstName, LastName, UserId, Email, MiddleName, LastUpdate, ActivationKey,
									 DateCreated, NewEmail, Email2, Gender, NameSuffix, MobileNumber, TelephoneNumber, LastLogin, StatusText,
									 PasswordExpiryDate, PhotoPath, ProviderId, [Status], MaritalStatusId, LastLoginFailureDate,
									 LoginFailureCount)
			VALUES				(@UserName,@Password,@FirstName,@LastName,@UserId, @Email, @MiddleName, @LastUpdate, @ActivationKey,
									@DateCreated, @NewEmail, @Email2, @Gender, @NameSuffix, @MobileNumber, @TelephoneNumber, @LastLogin,
									@StatusText, @PasswordExpiryDate, @PhotoPath, @ProviderId, @Status, @MaritalStatusId, @LastLoginFailureDate,
									@LoginFailureCount)
		end
	
	SELECT @UserId;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

