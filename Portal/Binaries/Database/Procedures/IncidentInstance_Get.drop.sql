
if exists (select * from dbo.sysobjects where id = object_id(N'[IncidentInstance_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [IncidentInstance_Get]
GO


