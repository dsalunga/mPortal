CREATE PROCEDURE dbo.IncidentTicket_Set
	(
		@Id int =-1,
		@UserId int,
		@DateCreated datetime,
		@CategoryId int,
		@Description ntext,
		@Urgency int,
		@Status int,
		@AssignedGroupId int,
		@AssignedUserId int,
		@TicketGuid nvarchar(50),
		@DateClosed datetime,
		@SubmitterId int,
		@TypeId int,
		@ETA datetime,
		@NotifyAlso ntext
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentTicket
			SET              UserId = @UserId, DateCreated = @DateCreated, CategoryId = @CategoryId, Description = @Description, Urgency = @Urgency, 
							Status = @Status, AssignedGroupId = @AssignedGroupId, AssignedUserId = @AssignedUserId, TicketGuid=@TicketGuid,
							DateClosed=@DateClosed, SubmitterId=@SubmitterId, TypeId=@TypeId, ETA=@ETA, NotifyAlso=@NotifyAlso
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentTicket';

			INSERT INTO IncidentTicket
			                      (UserId, DateCreated, CategoryId, Description, Urgency, Status, AssignedGroupId, AssignedUserId, Id,
								  TicketGuid, DateClosed, SubmitterId, TypeId, ETA, NotifyAlso)
			VALUES     (@UserId,@DateCreated,@CategoryId,@Description,@Urgency,@Status,@AssignedGroupId,@AssignedUserId,@Id, 
						@TicketGuid, @DateClosed, @SubmitterId, @TypeId, @ETA, @NotifyAlso)
		END

	SELECT @Id

	RETURN