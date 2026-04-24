
if exists (select * from dbo.sysobjects where id = object_id(N'[WebContent_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebContent_Set]
GO


