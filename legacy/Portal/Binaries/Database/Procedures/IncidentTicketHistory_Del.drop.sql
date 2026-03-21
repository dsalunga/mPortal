
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentTicketHistory_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentTicketHistory_Del]
GO


