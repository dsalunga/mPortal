IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LocalCalendarCategories_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarCategories] DROP CONSTRAINT [DF_LocalCalendarCategories_TemplateId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarCategories]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarCategories]
GO
