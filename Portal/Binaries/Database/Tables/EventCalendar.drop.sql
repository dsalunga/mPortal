IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__EventCalendar]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendar] DROP CONSTRAINT [DF__EventCalendar]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendar]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendar]
GO
