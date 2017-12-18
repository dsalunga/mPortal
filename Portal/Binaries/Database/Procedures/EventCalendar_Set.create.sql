
-- Procedure EventCalendar_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.EventCalendar_Set
	(
		@Id int = -1,
		@Name nvarchar(250),
		@SiteId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    EventCalendar
			SET              Name = @Name, SiteId=@SiteId
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'EventCalendar';

			INSERT INTO EventCalendar
			                      (Id, Name, SiteId)
			VALUES     (@Id,@Name, @SiteId)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

