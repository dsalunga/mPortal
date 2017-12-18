
-- Procedure WebUserGroup_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebUserGroup_Del]
	(
		@Id int = -1,
		@GroupId int = -1,
		@ObjectId int = -1,
		@RecordId int = -1
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			DELETE FROM WebUserGroup
			WHERE Id = @Id
		END
	ELSE IF(@ObjectId > 0 AND @GroupId > 0 AND @RecordId > 0)
		BEGIN
			DELETE FROM WebUserGroup
			WHERE RecordId = @RecordId
				AND ObjectId=@ObjectId
				AND GroupId=@GroupId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

