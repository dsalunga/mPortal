
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarLocations_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarLocations_Get]
GO


