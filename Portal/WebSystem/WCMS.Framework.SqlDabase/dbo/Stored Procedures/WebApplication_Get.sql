CREATE PROCEDURE [dbo].[WebApplication_Get]
	@Id int = -1
AS
	SELECT Id, Name, AppKey, IpAddresses FROM WebApplication
RETURN 0