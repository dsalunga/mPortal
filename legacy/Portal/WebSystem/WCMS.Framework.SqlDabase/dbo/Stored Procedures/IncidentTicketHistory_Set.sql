CREATE PROCEDURE dbo.IncidentTicketHistory_Set
	(
		@Id int = -1,
		@TicketId int,
		@UserId int,
		@Content ntext,
		@DateCreated datetime,
		@Type int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentTicketHistory
			SET              TicketId = @TicketId, UserId = @UserId, [Content] = @Content, DateCreated = @DateCreated, Type = @Type
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentTicketHistory';

			INSERT INTO IncidentTicketHistory
			                      (TicketId, UserId, [Content], DateCreated, Type, Id)
			VALUES     (@TicketId,@UserId,@Content,@DateCreated,@Type,@Id)
		END

	SELECT @Id;

	RETURN