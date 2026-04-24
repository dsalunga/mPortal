IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarCategories]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarCategories]
GO
