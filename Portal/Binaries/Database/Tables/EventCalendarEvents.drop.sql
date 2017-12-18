IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarCategories_EventCategoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarCategories_EventCategoryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_ReminderBefore]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_ReminderBefore]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_LocationId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_LocationId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_Weekdays]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_Weekdays]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_BookLocation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_BookLocation]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_CalendarId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_CalendarId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_TemplateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarEvents_SendReminderVia]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarEvents] DROP CONSTRAINT [DF_EventCalendarEvents_SendReminderVia]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarEvents]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarEvents]
GO
