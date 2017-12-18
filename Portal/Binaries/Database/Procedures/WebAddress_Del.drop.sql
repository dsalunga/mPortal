
if exists (select * from dbo.sysobjects where id = object_id(N'[WebAddress_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebAddress_Del]
GO


