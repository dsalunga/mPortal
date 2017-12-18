
if exists (select * from dbo.sysobjects where id = object_id(N'[WebApplication_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebApplication_Get]
GO


