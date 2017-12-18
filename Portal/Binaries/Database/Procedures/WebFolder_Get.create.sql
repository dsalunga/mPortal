
-- Procedure WebFolder_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebFolder_Get
	(
		@Id int = -1,
		@ParentId int = -2,
		@Name nvarchar(250) = NULL,
		@SiteId int = -1,
		@ObjectId int =-1
	)
AS
	SET NOCOUNT ON
	
	SELECT        Id, Name, ParentId, ShareName, SiteId, ObjectId
	FROM            WebFolder
	WHERE	(@Id = -1 OR Id=@Id) AND
			(@ParentId=-2 OR ParentId=@ParentId) AND
			(@Name IS NULL OR Name=@Name)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

