CREATE PROCEDURE [dbo].[EventCalendarEvents_Set]
	(
		@EventId int = -1,
		@Subject nvarchar(500),
		@Message nvarchar(2500),
		@Location nvarchar(250),
		@StartDate datetime,
		@EndDate datetime,
		@CategoryId int,
		@RepeatUntil datetime,
		@ReminderTo nvarchar(4000),
		@ReminderBefore int,
		@RecurrenceId int,
		@LocationId int,
		@Weekdays int,
		@LastReminderSent datetime,
		@BookLocation int,
		@CalendarId int,
		@TemplateId int,
		@SendReminderVia int
	)
AS
	SET NOCOUNT ON
	
	IF(@EventId > 0)
		BEGIN
			-- Update
			
			UPDATE    EventCalendarEvents
			SET              Subject = @Subject, Message = @Message, Location = @Location, StartDate = @StartDate, EndDate = @EndDate, CategoryId = @CategoryId, 
			                      RepeatUntil = @RepeatUntil, ReminderTo = @ReminderTo, ReminderBefore = @ReminderBefore, RecurrenceId = @RecurrenceId,
			                      LocationId=@LocationId, Weekdays=@Weekdays, LastReminderSent=@LastReminderSent, BookLocation=@BookLocation, 
								  CalendarId=@CalendarId, TemplateId=@TemplateId, SendReminderVia=@SendReminderVia
			WHERE     (EventId = @EventId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @EventId = WebObject_NextId 'EventCalendarEvents'
			
			INSERT INTO EventCalendarEvents
			                      (Subject, Message, Location, StartDate, EndDate, CategoryId, RepeatUntil, ReminderTo, ReminderBefore, RecurrenceId, 
								  EventId, LocationId, Weekdays, LastReminderSent, BookLocation, CalendarId, TemplateId, SendReminderVia)
			VALUES     (@Subject,@Message,@Location,@StartDate, @EndDate, @CategoryId,@RepeatUntil,@ReminderTo,@ReminderBefore,@RecurrenceId,@EventId, 
						@LocationId, @Weekdays, @LastReminderSent, @BookLocation, @CalendarId, @TemplateId, @SendReminderVia)
		END
	
	SELECT @EventId
	
	RETURN