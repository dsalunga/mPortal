
-- Procedure EventCalendarLocations_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarLocations_Set]
	(
		@LocationId int = -1,
		@Name nvarchar(250),
		@Bookable int
	)
AS
	SET NOCOUNT ON
	
	IF(@LocationId > 0)
		BEGIN
			-- Update
			
			UPDATE    EventCalendarLocations
			SET              Name = @Name, Bookable=@Bookable
			WHERE     (LocationId = @LocationId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @LocationId = WebObjects_NextId 'EventCalendarLocations'
			
			INSERT INTO EventCalendarLocations
			                      (Name, LocationId, Bookable)
			VALUES     (@Name,@LocationId, @Bookable)
		END
		
	SELECT @LocationId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

