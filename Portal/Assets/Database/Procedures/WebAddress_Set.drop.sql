
if exists (select * from dbo.sysobjects where id = object_id(N'[WebAddress_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebAddress_Set]
GO


