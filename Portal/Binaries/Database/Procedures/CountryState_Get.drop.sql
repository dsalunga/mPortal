
if exists (select * from dbo.sysobjects where id = object_id(N'[CountryState_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [CountryState_Get]
GO


