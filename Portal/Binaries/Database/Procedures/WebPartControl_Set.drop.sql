
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartControl_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartControl_Set]
GO


