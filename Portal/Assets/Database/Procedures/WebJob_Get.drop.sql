
if exists (select * from dbo.sysobjects where id = object_id(N'[WebJob_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebJob_Get]
GO


