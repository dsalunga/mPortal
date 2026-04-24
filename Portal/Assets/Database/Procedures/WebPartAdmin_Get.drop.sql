
if exists (select * from dbo.sysobjects where id = object_id(N'[WebPartAdmin_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebPartAdmin_Get]
GO


