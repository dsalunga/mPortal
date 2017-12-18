
if exists (select * from dbo.sysobjects where id = object_id(N'[WebRegistry_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebRegistry_Del]
GO


