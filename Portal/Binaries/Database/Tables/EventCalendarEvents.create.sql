SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventCalendarEvents](
	[EventId] [int] NOT NULL,
	[Subject] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Message] [nvarchar](2500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Location] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[RepeatUntil] [datetime] NOT NULL,
	[ReminderTo] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ReminderBefore] [int] NOT NULL,
	[RecurrenceId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[Weekdays] [int] NOT NULL,
	[LastReminderSent] [datetime] NOT NULL,
	[BookLocation] [int] NOT NULL,
	[CalendarId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[SendReminderVia] [int] NOT NULL,
 CONSTRAINT [PK_EventCalendarEvents] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarCategories_EventCategoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarCategories_EventCategoryId]  DEFAULT ((-1)) FOR [CategoryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_ReminderBefore]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_ReminderBefore]  DEFAULT ((-1)) FOR [ReminderBefore]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_LocationId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_LocationId]  DEFAULT ((-1)) FOR [LocationId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_Weekdays]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_Weekdays]  DEFAULT ((-1)) FOR [Weekdays]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_BookLocation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_BookLocation]  DEFAULT ((0)) FOR [BookLocation]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_CalendarId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_CalendarId]  DEFAULT ((-1)) FOR [CalendarId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_TemplateId]  DEFAULT ((-1)) FOR [TemplateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_SendReminderVia]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] ADD  CONSTRAINT [DF_EventCalendarEvents_SendReminderVia]  DEFAULT ((2)) FOR [SendReminderVia]
END

GO
