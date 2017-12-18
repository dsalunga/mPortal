
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendar_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendar_Get]
GO


