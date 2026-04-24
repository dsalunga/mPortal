
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartAdmin_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartAdmin_Del]
GO


