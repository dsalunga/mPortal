
if exists (select * from dbo.sysobjects where id = object_id(N'[WebGroup_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebGroup_Get]
GO


