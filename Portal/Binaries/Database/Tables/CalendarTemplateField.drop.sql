IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalendarTemplateField]') AND type in (N'U'))
DROP TABLE [dbo].[CalendarTemplateField]
GO
