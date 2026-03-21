
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartConfig_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartConfig_Set]
GO


