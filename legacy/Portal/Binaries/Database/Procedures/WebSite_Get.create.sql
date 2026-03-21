
-- Procedure WebSite_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebSite_Get]
	(
		@SiteId int = -1,
		@ParentId int = -2,
		@Identity nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT     SiteId, Name, Rank, Active, [Identity], Title, ParentId, HomePageId, DefaultMasterPageId, 
				HostName, PublicAccess, LoginPage, AccessDeniedPage, PageTitleFormat, ManagementAccess,
				BaseAddress, ThemeId, SkinId, PrimaryIdentityId
	FROM         WebSite
	WHERE     (@SiteId = - 1 OR SiteId = @SiteId) 
		AND (@ParentId = - 2 OR ParentId = @ParentId)
		AND (@Identity IS NULL OR [Identity] = @Identity)
	ORDER BY Rank
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

