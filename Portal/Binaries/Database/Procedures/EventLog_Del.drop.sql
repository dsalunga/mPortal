
if exists (select * from dbo.sysobjects where id = object_id(N'[EventLog_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [EventLog_Del]
GO


