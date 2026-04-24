
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentType_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentType_Set]
GO


