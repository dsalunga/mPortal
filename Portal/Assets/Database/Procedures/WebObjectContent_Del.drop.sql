
if exists (select * from dbo.sysobjects where id = object_id(N'[WebObjectContent_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebObjectContent_Del]
GO


