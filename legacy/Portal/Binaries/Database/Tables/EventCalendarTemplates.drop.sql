IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarTemplates]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarTemplates]
GO
