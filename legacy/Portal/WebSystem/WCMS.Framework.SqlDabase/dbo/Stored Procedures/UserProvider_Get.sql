CREATE PROCEDURE [dbo].[UserProvider_Get]
	@Id int = -1,
	@Name nvarchar(500) = NULL
AS
	SELECT Id, Name, ProviderName FROM UserProvider
	WHERE 
			(@Id=-1 OR Id=@Id)
		AND (@Name IS NULL OR Name=@Name)
	ORDER BY Name

RETURN 0