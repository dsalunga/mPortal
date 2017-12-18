
if exists (select * from dbo.sysobjects where id = object_id(N'[EventCalendarCategories_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventCalendarCategories_Get]
GO


