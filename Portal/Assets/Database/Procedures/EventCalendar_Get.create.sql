
-- Procedure EventCalendar_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.EventCalendar_Get
	(
		@Id int = -1,
		@SiteId int = -2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, SiteId
	FROM         EventCalendar
	WHERE     
		(@Id = -1 OR Id = @Id)
		AND (@SiteId=-2 OR SiteId=@SiteId)
	ORDER BY Name

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

