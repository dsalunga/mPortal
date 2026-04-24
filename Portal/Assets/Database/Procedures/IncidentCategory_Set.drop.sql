
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentCategory_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentCategory_Set]
GO


