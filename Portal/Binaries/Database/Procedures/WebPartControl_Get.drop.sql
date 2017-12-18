
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartControl_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartControl_Get]
GO


