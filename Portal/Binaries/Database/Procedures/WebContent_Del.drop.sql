
if exists (select * from dbo.sysobjects where id = object_id(N'[WebContent_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebContent_Del]
GO


