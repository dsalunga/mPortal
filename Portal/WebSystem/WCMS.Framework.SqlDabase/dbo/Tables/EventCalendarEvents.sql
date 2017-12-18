CREATE TABLE [dbo].[EventCalendarEvents] (
    [EventId]          INT             NOT NULL,
    [Subject]          NVARCHAR (500)  NOT NULL,
    [Message]          NVARCHAR (2500) NOT NULL,
    [Location]         NVARCHAR (250)  NOT NULL,
    [StartDate]        DATETIME        NOT NULL,
    [EndDate]          DATETIME        NOT NULL,
    [CategoryId]       INT             CONSTRAINT [DF_EventCalendarCategories_EventCategoryId] DEFAULT ((-1)) NOT NULL,
    [RepeatUntil]      DATETIME        NOT NULL,
    [ReminderTo]       NVARCHAR (4000) NOT NULL,
    [ReminderBefore]   INT             CONSTRAINT [DF_EventCalendarEvents_ReminderBefore] DEFAULT ((-1)) NOT NULL,
    [RecurrenceId]     INT             NOT NULL,
    [LocationId]       INT             CONSTRAINT [DF_EventCalendarEvents_LocationId] DEFAULT ((-1)) NOT NULL,
    [Weekdays]         INT             CONSTRAINT [DF_EventCalendarEvents_Weekdays] DEFAULT ((-1)) NOT NULL,
    [LastReminderSent] DATETIME        NOT NULL,
    [BookLocation]     INT             CONSTRAINT [DF_EventCalendarEvents_BookLocation] DEFAULT ((0)) NOT NULL,
    [CalendarId]       INT             CONSTRAINT [DF_EventCalendarEvents_CalendarId] DEFAULT ((-1)) NOT NULL,
    [TemplateId]       INT             CONSTRAINT [DF_EventCalendarEvents_TemplateId] DEFAULT ((-1)) NOT NULL,
    [SendReminderVia]  INT             CONSTRAINT [DF_EventCalendarEvents_SendReminderVia] DEFAULT ((2)) NOT NULL,
    CONSTRAINT [PK_EventCalendarEvents] PRIMARY KEY CLUSTERED ([EventId] ASC)
);

