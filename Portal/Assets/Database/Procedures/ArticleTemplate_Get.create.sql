
-- Procedure ArticleTemplate_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ArticleTemplate_Get]
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, Date, [File], ImageUrl, ListItemTemplate, ListTemplate, DetailsTemplate, DateFormat
	FROM         ArticleTemplate
	WHERE
		(@Id=-1 OR Id=@Id)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

