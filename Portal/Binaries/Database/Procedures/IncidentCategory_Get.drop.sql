
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentCategory_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentCategory_Get]
GO


