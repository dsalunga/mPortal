
if exists (select * from dbo.sysobjects where id = object_id(N'[WebParameter_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebParameter_Set]
GO


