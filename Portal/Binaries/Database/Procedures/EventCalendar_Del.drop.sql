
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendar_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendar_Del]
GO


