IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarRecurrences_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarRecurrences] DROP CONSTRAINT [DF_EventCalendarRecurrences_Rank]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarRecurrences]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarRecurrences]
GO
