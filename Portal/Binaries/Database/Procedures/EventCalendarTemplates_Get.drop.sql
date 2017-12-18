
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarTemplates_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarTemplates_Get]
GO


