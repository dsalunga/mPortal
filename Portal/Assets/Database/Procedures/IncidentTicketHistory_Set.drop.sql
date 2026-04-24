
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentTicketHistory_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentTicketHistory_Set]
GO


