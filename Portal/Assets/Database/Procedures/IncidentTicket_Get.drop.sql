
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentTicket_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentTicket_Get]
GO


