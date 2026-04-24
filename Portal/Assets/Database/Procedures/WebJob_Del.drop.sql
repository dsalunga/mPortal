
if exists (select * from dbo.sysobjects where id = object_id(N'[WebJob_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebJob_Del]
GO


