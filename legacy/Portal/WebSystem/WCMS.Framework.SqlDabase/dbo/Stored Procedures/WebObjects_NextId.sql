CREATE PROCEDURE [dbo].[WebObjects_NextId]
	(
	@TableName nvarchar(50)
	)
AS
	SET NOCOUNT ON
	declare @NextId int
	
	SET @NextId = (SELECT LastRecordId + 1 FROM WebObject 
		WHERE Name=@TableName)
	
	UPDATE WebObject SET LastRecordId=@NextId
		WHERE Name=@TableName
		
	RETURN @NextId