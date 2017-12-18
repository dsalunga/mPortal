
-- Procedure WebMasterPage_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebMasterPage_Get]
	(
		@MasterPageId int = -1,
		@SiteId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     MasterPageId, SiteId, TemplateId, Name, PublicAccess, OwnerPageId, ManagementAccess,
				ThemeId, SkinId, ParentId
	FROM         WebMasterPage
	WHERE     (@MasterPageId = - 1 OR
	                      MasterPageId = @MasterPageId)
	       AND (@SiteId = -1 OR SiteId = @SiteId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

