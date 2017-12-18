
-- Procedure ContactLink_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactLink_Get]
	(
		@SitePageItemID int,
		@PageType nchar(1)
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) SitePropertyID, ContactID, Mode
	FROM         SiteProperties
	WHERE     (SitePageItemID = @SitePageItemID) AND (PageType = @PageType)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

