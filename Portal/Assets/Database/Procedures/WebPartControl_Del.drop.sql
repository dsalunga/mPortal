
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartControl_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartControl_Del]
GO


