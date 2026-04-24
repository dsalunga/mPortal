
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarEvents_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarEvents_Set]
GO


