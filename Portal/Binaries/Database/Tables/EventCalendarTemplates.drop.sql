IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarTemplates_SmsContent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarTemplates] DROP CONSTRAINT [DF_EventCalendarTemplates_SmsContent]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarTemplates]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarTemplates]
GO
