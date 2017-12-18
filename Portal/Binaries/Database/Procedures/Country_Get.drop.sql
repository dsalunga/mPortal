
if exists (select * from dbo.sysobjects where id = object_id(N'[Country_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Country_Get]
GO


