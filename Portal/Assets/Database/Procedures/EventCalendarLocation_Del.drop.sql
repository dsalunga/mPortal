
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarLocation_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarLocation_Del]
GO


