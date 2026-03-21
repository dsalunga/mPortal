
-- Procedure IncidentTicketHistory_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentTicketHistory_Get
	(
		@Id int = -2,
		@TicketId int = -2,
		@UserId int =-2,
		@Type int = -2
	)
AS
	SET NOCOUNT ON

	SELECT     Id, TicketId, UserId, [Content], DateCreated, Type
	FROM         IncidentTicketHistory
	WHERE     (@Id =-2 OR Id = @Id) 
		AND (@TicketId =-2 OR TicketId = @TicketId) 
		AND (@UserId =-2 OR UserId = @UserId) 
		AND (@Type =-2 OR Type = @Type)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

