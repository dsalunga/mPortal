
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarCategory_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarCategory_Del]
GO


