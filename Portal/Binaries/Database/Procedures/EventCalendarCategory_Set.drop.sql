
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarCategory_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarCategory_Set]
GO


