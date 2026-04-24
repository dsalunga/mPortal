IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarLocations]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarLocations]
GO
