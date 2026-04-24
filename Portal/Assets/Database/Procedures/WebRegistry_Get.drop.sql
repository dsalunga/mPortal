
if exists (select * from dbo.sysobjects where id = object_id(N'[WebRegistry_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebRegistry_Get]
GO


