
if exists (select * from dbo.sysobjects where id = object_id(N'[WebGroup_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebGroup_Set]
GO


