IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarLocations_Bookable]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarLocations] DROP CONSTRAINT [DF_EventCalendarLocations_Bookable]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarLocations]') AND type in (N'U'))
DROP TABLE [dbo].[EventCalendarLocations]
GO
