IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarEvents]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarEvents]
GO
