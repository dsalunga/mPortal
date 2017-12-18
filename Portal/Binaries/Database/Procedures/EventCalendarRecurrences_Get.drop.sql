
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarRecurrences_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarRecurrences_Get]
GO


