
-- Procedure EventCalendarEvents_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarEvents_Get]
	(
		@EventId int = -1,
		
		@StartDateFrom datetime = null,
		@StartDateTo datetime = null,
		
		/*
		@ReminderFrom int = -1,
		@ReminderTo int = -1,
		*/
		
		@SelectType int = -1, -- 1->Get for calendar; 2->Get reminders
		@RepeatUntil datetime = null,
		@CalendarId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     EventId, Subject, Message, Location, StartDate, EndDate, CategoryId, RepeatUntil, 
			ReminderTo, ReminderBefore, RecurrenceId, LocationId, Weekdays, LastReminderSent,
			BookLocation, CalendarId, TemplateId, SendReminderVia
	FROM         EventCalendarEvents
	WHERE 
			(@EventId=-1 OR EventId=@EventId)

		AND (@CalendarId=-1 OR CalendarId=@CalendarId)

		AND	(@StartDateTo IS NULL OR @StartDateFrom IS NULL OR
				(RecurrenceId=-1 AND @StartDateFrom<=StartDate AND StartDate<=@StartDateTo) OR 
				(RecurrenceId > 0 AND (RepeatUntil>=@StartDateTo OR RepeatUntil>=@StartDateFrom))
			)
		
		/*
		AND	(@ReminderFrom = -1 OR @ReminderTo = -1 OR 
				(ReminderBefore>=@ReminderFrom AND ReminderBefore<=@ReminderTo)
			)
		*/
			
		AND	(@SelectType=-1 OR @RepeatUntil IS NULL OR
				(	
					-- Get events for a month
					(@SelectType=1 AND RepeatUntil>=@StartDateFrom AND @RepeatUntil>=StartDate)
					OR
					-- Get reminders
					(@SelectType=2 AND 
						ReminderBefore > -1
						AND
						(RecurrenceId=-1 OR @StartDateFrom<=RepeatUntil AND StartDate<=@RepeatUntil) -- Eval when recurrence
						AND
						(RecurrenceId>-1 OR StartDate>=@StartDateFrom AND EndDate<=@RepeatUntil) -- Eval when no recurrence
					)
					-- @RepeatUntil -> CurrentDate + MaxMinutes
				)
			)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

