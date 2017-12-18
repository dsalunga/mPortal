
if exists (select * from dbo.sysobjects where id = object_id(N'[WebMasterPage_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebMasterPage_Get]
GO


