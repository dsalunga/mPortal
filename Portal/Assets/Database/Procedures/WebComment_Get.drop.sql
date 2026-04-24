
if exists (select * from dbo.sysobjects where id = object_id(N'[WebComment_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebComment_Get]
GO


