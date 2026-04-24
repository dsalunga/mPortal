
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarTemplates_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarTemplates_Set]
GO


