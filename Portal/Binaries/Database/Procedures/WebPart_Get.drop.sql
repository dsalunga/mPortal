
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPart_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPart_Get]
GO


