IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendar]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendar]
GO
