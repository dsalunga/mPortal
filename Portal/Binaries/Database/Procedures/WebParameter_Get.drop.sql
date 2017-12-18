
if exists (select * from dbo.sysobjects where id = object_id(N'[WebParameter_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebParameter_Get]
GO


