CREATE PROCEDURE dbo.IncidentTicket_Get
	(
		@Id int = -2,
		@UserId int = -2,
		@CategoryId int = -2,
		@Status int = -2,
		@Urgency int = -2,
		@AssignedGroupId int =-2,
		@AssignedUserId int = -2,
		@TicketGuid nvarchar(50) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT     Id, UserId, DateCreated, CategoryId, Description, Urgency, Status, AssignedGroupId, AssignedUserId,
			TicketGuid, DateClosed, SubmitterId, TypeId, ETA, NotifyAlso
	FROM         IncidentTicket
	WHERE     (@Id=-2 OR Id = @Id) 
		AND (@UserId=-2 OR UserId = @UserId) 
		AND (@CategoryId=-2 OR CategoryId = @CategoryId) 
		AND (@Urgency=-2 OR Urgency = @Urgency) 
		AND (@Status=-2 OR Status = @Status) 
		AND (@AssignedGroupId=-2 OR AssignedGroupId = @AssignedGroupId) 
		AND (@AssignedUserId=-2 OR AssignedUserId = @AssignedUserId)
		AND (@TicketGuid IS NULL OR TicketGuid=@TicketGuid)

	RETURN