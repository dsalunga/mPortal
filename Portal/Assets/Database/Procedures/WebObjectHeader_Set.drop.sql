
if exists (select * from dbo.sysobjects where id = object_id(N'[WebObjectHeader_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebObjectHeader_Set]
GO


