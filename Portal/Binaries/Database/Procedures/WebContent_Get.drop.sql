
if exists (select * from dbo.sysobjects where id = object_id(N'[WebContent_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebContent_Get]
GO


