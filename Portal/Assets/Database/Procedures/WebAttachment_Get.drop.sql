
if exists (select * from dbo.sysobjects where id = object_id(N'[WebAttachment_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebAttachment_Get]
GO


