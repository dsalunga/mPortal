
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarEvents_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarEvents_Get]
GO


