
if exists (select * from dbo.sysobjects where id = object_id(N'[WebObject_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebObject_Del]
GO


