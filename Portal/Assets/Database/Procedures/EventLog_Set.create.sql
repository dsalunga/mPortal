
-- Procedure EventLog_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.EventLog_Set
	(
		@Id int = -1,
		@EventDate datetime,
		@Content ntext,
		@UserId int,
		@EventName nvarchar(250),
		@IPAddress nvarchar(50)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    EventLog
			SET              EventDate = @EventDate, [Content] = @Content, UserId = @UserId, EventName = @EventName, IPAddress = @IPAddress
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'EventLog'

			INSERT INTO EventLog
								  (Id, EventDate, [Content], UserId, EventName, IPAddress)
			VALUES     (@Id,@EventDate,@Content,@UserId,@EventName,@IPAddress)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

