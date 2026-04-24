
if exists (select * from dbo.sysobjects where id = object_id(N'[WebJob_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebJob_Set]
GO


