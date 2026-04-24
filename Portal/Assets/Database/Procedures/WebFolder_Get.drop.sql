
if exists (select * from dbo.sysobjects where id = object_id(N'[WebFolder_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebFolder_Get]
GO


