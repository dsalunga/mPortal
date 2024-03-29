
-- Procedure WebTemplate_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebTemplate_Get]
	(
		@Id int = -1,
		@ThemeId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, FileName, [Identity], PrimaryPanelId, Version,
				VersionOf, Content, DateModified, ThemeId, Standalone, ParentId,
				SkinId, TemplateEngineId
	FROM         WebTemplate
	WHERE   (@Id = - 1 OR Id = @Id)
		AND (@ThemeId=-2 OR ThemeId=@ThemeId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

