
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarTemplate_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarTemplate_Del]
GO


