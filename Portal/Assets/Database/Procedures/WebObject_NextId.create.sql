
-- Procedure WebObject_NextId
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebObject_NextId]
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

