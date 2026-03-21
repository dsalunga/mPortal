
if exists (select * from dbo.sysobjects where id = object_id(N'[WebMessageQueue_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebMessageQueue_Set]
GO


